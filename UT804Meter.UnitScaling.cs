using System;

namespace UT804DMM
{

    public partial class UT804Meter
    {
        class UnitScaling
        {
            public readonly string units;
            public readonly decimal valueMuntiplyer = 1;

            public UnitScaling(SwitchPositions switchPosition, CouplingFlags coupling, StatusFlags status, int range)
            {
                bool negertiveValue = status.HasFlag(StatusFlags.neg);

                //todo could add lots of data validation here to check settings match behavior but beyond the scope of intial version

                switch (switchPosition)
                {
                    case SwitchPositions.V:
                        units = "V";
                        valueMuntiplyer = 1 / (decimal)Math.Pow(10, 5 - range);
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.VAC:
                        units = "V AC";
                        valueMuntiplyer = 1 / (decimal)Math.Pow(10, 5 - range);
                        break;

                    case SwitchPositions.mV:
                        units = "mV";
                        valueMuntiplyer = 0.01M;
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.Ohms:
                        switch (range)
                        {
                            case 1:
                                units = "Ohms";
                                valueMuntiplyer = 0.01M;
                                break;
                            case 2:
                                units = "KOhms";
                                valueMuntiplyer = 0.0001M;
                                break;
                            case 3:
                                units = "KOhms";
                                valueMuntiplyer = 0.001M;
                                break;
                            case 4:
                                units = "KOhms";
                                valueMuntiplyer = 0.01M;
                                break;
                            case 5:
                                units = "MOhms";
                                valueMuntiplyer = 0.0001M;
                                break;
                            case 6:
                                units = "MOhms";
                                valueMuntiplyer = 0.001M;
                                break;
                            case 7:
                                units = "MOhms";
                                valueMuntiplyer = 0.01M;
                                break;
                        }
                        break;

                    case SwitchPositions.F:
                        //meter doen't like this mode stops outputing values to RS232 not sure why
                        units = "F";
                        //todo
                        break;

                    case SwitchPositions.degC:
                        units = "°C";
                        valueMuntiplyer = 0.1M;
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.uA:
                        units = "uA";
                        valueMuntiplyer = 1 / (decimal)Math.Pow(10, 2 - range);
                        if (negertiveValue & !coupling.HasFlag(CouplingFlags.AC))
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.mA:
                        units = "mA";
                        valueMuntiplyer = 1 / (decimal)Math.Pow(10, 3 - range);
                        if (negertiveValue & !coupling.HasFlag(CouplingFlags.AC))
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.A:
                        units = "A";
                        valueMuntiplyer = 0.001M;
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.BEEP:
                        units = "BEEP";
                        //todo - future task not important
                        break;

                    case SwitchPositions.DIODE:
                        units = "DIODE";
                        //todo - future task not important
                        break;

                    case SwitchPositions.HZdutycycle:
                        if (negertiveValue)
                        {
                            units = "duty cycle %";
                            valueMuntiplyer = 0.01M;
                        }
                        else
                        {
                            switch (range)
                            {
                                case 0:
                                    units = "Hz";
                                    valueMuntiplyer = 0.001M;
                                    break;
                                case 1:
                                    units = "Hz";
                                    valueMuntiplyer = 0.01M;
                                    break;
                                case 2:
                                    units = "KHz";
                                    valueMuntiplyer = 0.0001M;
                                    break;
                                case 3:
                                    units = "KHz";
                                    valueMuntiplyer = 0.001M;
                                    break;
                                case 4:
                                    units = "KHz";
                                    valueMuntiplyer = 0.01M;
                                    break;
                                case 5:
                                    units = "MHz";
                                    valueMuntiplyer = 0.0001M;
                                    break;
                                case 6:
                                    units = "MHz";
                                    valueMuntiplyer = 0.001M;
                                    break;
                                case 7:
                                    units = "MHz";
                                    valueMuntiplyer = 0.01M;
                                    break;
                            }
                        }
                        break;

                    case SwitchPositions.degF:
                        units = "°F";
                        valueMuntiplyer = 0.1M;
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    case SwitchPositions.Percent4To20mA:
                        units = "%";
                        valueMuntiplyer = 0.01M;
                        if (negertiveValue)
                            valueMuntiplyer *= -1;
                        break;

                    default:
                        throw (new ApplicationException("switch positon given is invaid"));
                }
            }
        }
    }
}
