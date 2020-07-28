using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OrderProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                // Display message to user to provide parameters.
                System.Console.WriteLine("Please enter parameter values.");
                Console.Read();
            }
            else
            {
                List<List<Order>> lOrders = new List<List<Order>>();

                for (int i = 0; i < args.Length; i++)
                {
                    List<Order> litems = new List<Order>();
                    // Loop through array to list args parameters.
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("OUTPUT" + (i + 1) + ":");
                    Console.Write(Environment.NewLine);
                    Console.ForegroundColor = ConsoleColor.White;
                
                    string s = args[i];
                    string[] values = s.Split(',');
                    double taxesPlaceHolder = 0;
                    double total = 0;
                    foreach (var item in values)
                    {
                        string[] orderitems = item.Split(';');
                       
                        Order ord = new Order()
                        {
                            quantity = Convert.ToInt32( orderitems[0]),
                            orderName = orderitems[1],
                            rowPrice = Convert.ToDouble( orderitems[2]),
                            imported = Convert.ToBoolean(orderitems[3]),
                            taxable = Convert.ToBoolean(orderitems[4])
                        };
                        //Initializing the order processor with concrete classes 
                        //still loosely coupled senseit it is in the program file (main entry point)
                        //OrderProcessor class dosn't depend on TaxCalculator and  ImportFeeCalculator classes
                        // SOLID principle interfaces and extensibility
                        var orderp = new OrderProcessor(new TaxCalculator(),new ImportFeeCalculator());

                         Order processedOrder = orderp.ProcessOrder(ord);
                        ord.finalPrice = processedOrder.finalPrice;
                        ord.taxes =  processedOrder.taxes;
                        taxesPlaceHolder = taxesPlaceHolder + processedOrder.taxes;
                        total = total + Convert.ToDouble(processedOrder.finalPrice);
                        bool alreadyExist = litems.FirstOrDefault(x => x.orderName == processedOrder.orderName && x.finalPrice == processedOrder.finalPrice)== null?false:true;
                       if(!alreadyExist)
                        {
                            litems.Add(ord);
                        }
                        else
                        {
                            Order exIrder = litems.FirstOrDefault(x => x.orderName == processedOrder.orderName && x.finalPrice == processedOrder.finalPrice);
                            exIrder.quantity = exIrder.quantity + 1;
              
                        }
                   
                    }
                    foreach (var item in litems)
                    {
                        if(item.quantity > 1)
                        {
                            Console.WriteLine( item.orderName.Replace('_',' ') + ":" + " $" +(Convert.ToDouble(item.finalPrice) * (int)item.quantity).ToString("0.00") + " (" + (item.quantity + " @ $" + (Convert.ToDouble(item.finalPrice) ) + " )"));
                        }
                        else
                        {
                            Console.WriteLine(item.orderName.Replace('_', ' ') + ":" + " $" + (Convert.ToDouble(item.finalPrice) * (int)item.quantity));

                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                     Console.WriteLine("Sales Taxes:" + " $" + taxesPlaceHolder.ToString("0.00"));
                     Console.WriteLine("Total:" + " $" + total.ToString("0.00"));
                     Console.Write(Environment.NewLine);
                    lOrders.Add(litems);
                }
                Console.Read();
            }

        }
    }
}
