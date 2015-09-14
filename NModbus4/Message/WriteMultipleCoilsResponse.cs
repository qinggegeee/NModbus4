﻿namespace Modbus.Message
{
    using System;
    using System.Net;

    /// <summary>
    ///
    /// </summary>
    public class WriteMultipleCoilsResponse : AbstractModbusMessage, IModbusMessage
    {
        /// <summary>
        ///
        /// </summary>
        public WriteMultipleCoilsResponse()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="slaveAddress"></param>
        /// <param name="startAddress"></param>
        /// <param name="numberOfPoints"></param>
        public WriteMultipleCoilsResponse(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            : base(slaveAddress, Modbus.WriteMultipleCoils)
        {
            StartAddress = startAddress;
            NumberOfPoints = numberOfPoints;
        }

        /// <summary>
        ///
        /// </summary>
        public ushort NumberOfPoints
        {
            get
            {
                return MessageImpl.NumberOfPoints.Value;
            }
            set
            {
                if (value > Modbus.MaximumDiscreteRequestResponseSize)
                {
                    string msg = $"Maximum amount of data {Modbus.MaximumDiscreteRequestResponseSize} coils.";
                    throw new ArgumentOutOfRangeException("NumberOfPoints", msg);
                }

                MessageImpl.NumberOfPoints = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public ushort StartAddress
        {
            get { return MessageImpl.StartAddress.Value; }
            set { MessageImpl.StartAddress = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public override int MinimumFrameSize
        {
            get { return 6; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string msg = $"Wrote {NumberOfPoints} coils starting at address {StartAddress}.";
            return msg;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="frame"></param>
        protected override void InitializeUnique(byte[] frame)
        {
            StartAddress = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 2));
            NumberOfPoints = (ushort)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(frame, 4));
        }
    }
}
