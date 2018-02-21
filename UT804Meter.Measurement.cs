using System;

namespace UT804DMM
{
    public partial class UT804Meter
    {
        public class Measurement
        {
            public readonly decimal value;
            public readonly string nonNumvalue;
            public readonly string units;
            public readonly int range;
            public readonly SwitchPositions SwitchPosition;
            public readonly CouplingFlags coupling;
            public readonly StatusFlags status;
            public readonly string packet;

            public Measurement(String meterPacket)
            {
                packet = meterPacket;
                if (meterPacket.Length != 9)
                    throw (new ApplicationException("incorrect format input string  should be 9 char long"));
                //todo validate string content where posable (vaild char set ? 0,1,2,3,4,5,6,7,8,9,:,H,<,....)
                var displayDigits = meterPacket.Substring(0, 5);
                //convert range char to number
                range = (0x0f & meterPacket[5]);
                SwitchPosition = (SwitchPositions)meterPacket[6];
                coupling = (CouplingFlags)(0x0f & meterPacket[7]);
                status = (StatusFlags)(0x0f & meterPacket[8]);

                var measInfo = new UT804Meter.UnitScaling(SwitchPosition, coupling, status , range);
                units = measInfo.units;
                if (displayDigits[0] == '<' || displayDigits[0] == '?' || displayDigits[0] == ':')
                {
                    nonNumvalue = displayDigits;
                    value = 99999;
                }
                else
                {
                    nonNumvalue = null;
                    try
                    {
                        value = decimal.Parse(displayDigits)*measInfo.valueMuntiplyer;
                    }
                    catch (FormatException)
                    {
                        throw (new ApplicationException("digits string dosn't contail the correct format"));
                    }
                }


            }
        }
    }
}
