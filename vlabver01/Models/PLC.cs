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

        public string Name { get; set; }
        public string Description { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string ImgPath { get; set; }

        public void Connect() // test
        {
            ModbusClient client = new ModbusClient(IPAddress, Port);
            client.Connect();


            client.Disconnect();
        }

        public bool[] Read()
        {
            ModbusClient client = new ModbusClient(IPAddress, Port);
            client.Connect();
            var coils = client.ReadCoils(0, 10);
            client.Disconnect();

            return coils;
        }


    }
}