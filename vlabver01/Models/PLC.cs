using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyModbus;

namespace vlabver01.Models
{
    public class PLC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        //[Index(IsUnique = true)]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Adres IP")]
        [RegularExpression(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Niepoprawny adres IP")]
        public string IPAddress { get; set; }

        [Display(Name = "Port")]
        public int Port { get; set; }

        public string ImgPath { get; set; }

        public void Connect() // test
        {
            ModbusClient client = new ModbusClient(IPAddress, Port);
            client.Connect();


            client.Disconnect();
        }


        /*
        // read backup
        public bool[] Read(int startingAddress, int quantity)
        {
            ModbusClient client = new ModbusClient(IPAddress, Port);
            client.Connect();
            var val = client.ReadCoils(startingAddress, quantity);
            client.Disconnect();

            return val;
        }
        */


        // testf, do usuniecia ..
        public bool[] Read(int startingAddress, int quantity)
        {
            bool[] val = new bool[quantity];
            try
            {
                ModbusClient client = new ModbusClient(IPAddress, Port);

                client.Connect();
                val = client.ReadCoils(startingAddress, quantity);
                client.Disconnect();
            }
            catch
            {

            }


            return val;
        }



        // read %Q 
        public bool[] ReadQ(int startingAddress, int quantity)
        {
            bool[] val = new bool[quantity];
            try
            {
                ModbusClient client = new ModbusClient(IPAddress, Port);

                client.Connect();
                val = client.ReadCoils(startingAddress, quantity);
                client.Disconnect();
            }
            catch
            {

            }
            

            return val;
        }


        // read %I
        public bool[] ReadI(int startingAddress, int quantity)
        {
            bool[] val = new bool[quantity];
            try
            {
                ModbusClient client = new ModbusClient(IPAddress, Port);

                client.Connect();
                val = client.ReadDiscreteInputs(startingAddress, quantity);
                client.Disconnect();
            }
            catch
            {

            }

            return val;
        }

        // read %R
        public int[] ReadR(int startingAddress, int quantity)
        {
            int[] val = new int[quantity];
            try
            {
                ModbusClient client = new ModbusClient(IPAddress, Port);

                client.Connect();
                val = client.ReadHoldingRegisters(startingAddress, quantity);
                client.Disconnect();
            }
            catch
            {

            }


            return val;
        }


        // read %AI
        public int[] ReadAI(int startingAddress, int quantity)
        {
            int[] val = new int[quantity];
            try
            {
                ModbusClient client = new ModbusClient(IPAddress, Port);

                client.Connect();
                val = client.ReadInputRegisters(startingAddress, quantity);
                client.Disconnect();
            }
            catch
            {

            }


            return val;
        }


    }
}