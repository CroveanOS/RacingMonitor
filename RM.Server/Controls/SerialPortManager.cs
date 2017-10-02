using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;

namespace RM.Server.Controls
{
    /// <summary>
    /// Manager for serial port data
    /// </summary>
    public class SerialPortManager : IDisposable
    {
        private SerialPort _serialPort;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved;
        public SerialSettings CurrentSerialSettings { get; set; } = new SerialSettings();

        public SerialPortManager()
        {
            // Finding installed serial ports on hardware
            CurrentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
            CurrentSerialSettings.PropertyChanged += _currentSerialSettings_PropertyChanged;

            // If serial ports is found, we select the first found
            if (CurrentSerialSettings.PortNameCollection.Length > 0)
                CurrentSerialSettings.PortName = CurrentSerialSettings.PortNameCollection[0];
        }

        ~SerialPortManager()
        {
            Dispose(false);
        }

        private void _currentSerialSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // if serial port is changed, a new baud query is issued
            if (e.PropertyName.Equals("PortName"))
                UpdateBaudRateCollection();
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int dataLength = _serialPort.BytesToRead;
            byte[] data = new byte[dataLength];
            int nbrDataRead = _serialPort.Read(data, 0, dataLength);
            if (nbrDataRead == 0)
                return;

            NewSerialDataRecieved?.Invoke(this, new SerialDataEventArgs(data));
        }

        public void StartListening()
        {
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();

            if (SetConnection())
            {
                _serialPort.DataReceived += _serialPort_DataReceived;
                Open(_serialPort);
                _serialPort.RtsEnable = true;
            }
        }

        public Boolean SetConnection()
        {
            try
            {
                CurrentSerialSettings.GetSettings();

                _serialPort = new SerialPort(
                   CurrentSerialSettings.PortName,
                   CurrentSerialSettings.BaudRate,
                   CurrentSerialSettings.Parity,
                   CurrentSerialSettings.DataBits,
                   CurrentSerialSettings.StopBits);
                
                DecoderState.IsConnected = true;
                //_serialPort = new SerialPort(CurrentSerialSettings.PortName, 8, Parity.Even, 16, StopBits.Two);
                return true;
            }
            catch
            {
                DecoderState.IsConnected = false;
                return false;
            }
        }

        private void Open(SerialPort sp)
        {
            if(sp != null)
            {
                sp.Open();
                DecoderState.IsConnected = true;
            }
        }

        public void StopListening()
        {
            _serialPort?.Close();
        }
        
        private void UpdateBaudRateCollection()
        {
            _serialPort = new SerialPort(CurrentSerialSettings.PortName);
            Open(_serialPort);
            object p = _serialPort.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_serialPort.BaseStream);
            var value = p?.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.GetValue(p);
            if (value == null) return;
            int dwSettableBaud = (int)value;

            _serialPort.Close();
            CurrentSerialSettings.UpdateBaudRateCollection(dwSettableBaud);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_serialPort == null) return;
            if (disposing)
            {
                _serialPort.DataReceived -= _serialPort_DataReceived;
            }

            if (_serialPort.IsOpen) 
                _serialPort.Close();

            _serialPort.Dispose();
        }
    }

    public class SerialDataEventArgs : EventArgs
    {
        public SerialDataEventArgs(byte[] dataInByteArray)
        {
            Data = dataInByteArray;
        }

        public byte[] Data;
    }
}
