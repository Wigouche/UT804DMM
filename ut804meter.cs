using System;
using System.IO.Ports;

namespace UT804DMM
{
    public enum SwitchPositions
    {
        V = '1',
        VAC = '2',
        mV = '3',
        Ohms = '4',
        F = '5',
        degC = '6',
        uA = '7',
        mA = '8',
        A = '9',
        BEEP = (char)0x3A,
        DIODE = (char)0x3B,
        HZdutycycle = (char)0x3C,
        degF = (char)0x3D,
        Percent4To20mA = (char)0x3F
    };

    [Flags]
    public enum StatusFlags
    {
        neg = 0x04,
        autoRange = 0x01,
        manualRange = 0x02,
    };

    [Flags]
    public enum CouplingFlags
    {
        AC = 0x01,
        DC = 0x02,
    };

    public partial class UT804Meter : IDisposable
    {
        SerialPort Port;

        public UT804Meter(string portName)
        {
            Port = new SerialPort
            {
                PortName = portName,
                BaudRate = 2400,
                //this string is the end of line gnersted by the meter
                NewLine = "\r\n",
                DataBits = 7,
                Parity = Parity.Odd,
                // Set the read/write timeouts
                ReadTimeout = 2000,
                WriteTimeout = 2000,
            };

            Port.Open();

            Port.RtsEnable = true;
            Port.ReadExisting();
        }

        string GetMeasurementString()
        {
            Port.RtsEnable = false;
            var inputPacket = "";
            while (inputPacket.Length < 9)
            {
                inputPacket = Port.ReadLine();
            }
            inputPacket = inputPacket.Substring(inputPacket.Length - 9);
            Port.RtsEnable = true;

            return inputPacket;
        }

        public Measurement GetMeasurement()
        {
            return new Measurement(GetMeasurementString());
        }

        public void Dispose()
        {
            Port.Close();
        }
    }
}
