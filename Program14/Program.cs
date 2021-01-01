using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Program14
{
    class Program
    {
        static void Main(string[] args)
        {
            string restart = "yes";//why this is needed, I will explain at the end
            while (restart == "yes")
            {
                int i = 0;
                int sum = 0;
                string name;
                string path_prod = @"C:\Users\1392659\Desktop\prog14\products\";//2 ways, for orders and for products
                string path_order = @"C:\Users\1392659\Desktop\prog14\orders\";
                int qua1 = 0;//a large number of variables could be replaced with something else, but I don’t know how to do it
                int q1 = 0;
                int qua2 = 0;
                int q2 = 0;
                int qua3 = 0;
                int q3 = 0;
                string price;
                string id = "";
                string order_id = "1";
                while(File.Exists(path_order + order_id + ".txt"))//creating a new order if the old ID is already taken
                {
                    int a = Convert.ToInt32(order_id);
                    a++;
                    order_id = Convert.ToString(a);
                }
                Console.WriteLine("What do you want ?(create new order - create, check an existing order - check)");
                string answer = Console.ReadLine();
                while (((answer == "create") || (answer == "check")) != true)//I tried to fill the whole program with loops to prevent errors
                {
                    Console.WriteLine("Error, try again");
                    answer = Console.ReadLine();
                }
                if (answer == "check")//easy viewing of the order by ID
                {
                    Console.WriteLine("Enter order id:");
                    order_id = Console.ReadLine();
                    while (File.Exists((path_order + order_id + ".txt")) != true)//To avoid mistakes, I used the loop
                    {
                        Console.WriteLine("Error, try again");
                        order_id = Console.ReadLine();
                    }
                    using (StreamReader sr = new StreamReader(path_order + order_id + ".txt"))
                    {
                        while (true)
                        {
                            string j = sr.ReadLine();//small loop to output all lines of the file
                            if (j == null)
                            {
                                break;
                            }
                            Console.WriteLine(j);
                        }
                    }
                }
                if (answer == "create")//I tried to approach the solution from very different angles, so the code may seem very strange, but I will try to explain all aspects in as much detail as possible
                {
                    Console.WriteLine("Do you want to add product ?(yes)");
                    answer = Console.ReadLine();
                    if (answer != "yes")//if you answer something other than yes, then the creation program will end
                    {
                        Console.WriteLine("The order is empty or something written wrong");
                    }
                    while (answer == "yes")//the answer is needed to stop the loop
                    {
                        Console.WriteLine("Choose id of product:(1,2,3)");
                        id = Console.ReadLine();
                        while (((id == "1") || (id == "2") || (id == "3")) != true)//here you could probably use: (id >= 1)&&(id <=3),but sometimes it gave an error and I could not figure out what was wrong
                        {
                            Console.WriteLine("Choose correct id:");
                            id = Console.ReadLine();
                        }
                        using (StreamReader sr = new StreamReader(path_prod + id + ".txt"))
                        {//accessing the file and then trimming the content
                            sr.ReadLine();
                            name = sr.ReadLine().Remove(0, 6);
                            price = sr.ReadLine().Remove(0, 7);
                            switch (id)//the fun begins
                            {
                                case ("1"):
                                    if (q1 == 0)//I had a huge number of bugs, due to which the values went over the quantity of the product or did not display the product at all in the list
                                    {
                                        qua1 = Convert.ToInt32(sr.ReadLine().Remove(0, 9));
                                        q1 = qua1;//assigned values so that they affect the algorithm and store information in themselves at the same time
                                        qua1--;
                                    }
                                    else
                                    {
                                        if (qua1 == 0)//due to the fact that the product went into a negative number, I decided to return the required value to it if it reaches a certain value
                                            qua1++;//I think it counts as crutch, but the main thing is that it works
                                        qua1--;
                                    }
                                    break;
                                case ("2"):
                                    if (q2 == 0)
                                    {
                                        qua2 = Convert.ToInt32(sr.ReadLine().Remove(0, 9));
                                        q2 = qua2;
                                        qua2--;
                                    }
                                    else
                                    {
                                        if (qua2 == 0)
                                            qua2++;
                                        qua2--;
                                    }
                                    break;
                                case ("3"):
                                    if (q3 == 0)
                                    {
                                        qua3 = Convert.ToInt32(sr.ReadLine().Remove(0, 9));
                                        q3 = qua3;
                                        qua3--;
                                    }
                                    else
                                    {
                                        if (qua3 == 0)
                                            qua3++;
                                        qua3--;
                                    }
                                    break;
                            }
                            switch (id)
                            {
                                case ("1"):
                                    if (qua1 <= 0)//if the number of products is 0, then the value changes and in the future will cause the program to react
                                    {
                                        i++;
                                    }
                                    break;
                                case ("2"):
                                    if (qua2 <= 0)
                                    {
                                        i++;
                                    }
                                    break;
                                case ("3"):
                                    if (qua3 <= 0)
                                    {
                                        i++;
                                    }
                                    break;
                            }
                            using (StreamWriter sw = new StreamWriter(path_order + order_id + ".txt"))//
                            {//noticed this bug at the last moment, when almost everything was ready:
                                //only the name of the last product is recorded, but the rest are erased
                                //for this reason, I decided to use a simple method of solving the output through if
                                if (i == 0)
                                {
                                    if (File.Exists(order_id + ".txt") != true)
                                    {
                                        sw.WriteLine("Order id: " + order_id);
                                        sw.WriteLine(DateTime.Now);
                                        sw.WriteLine("Product list:");
                                        if(q1 != qua1)//it outputs all values at once that are not equal to zero
                                            sw.WriteLine("Milk " + (q1 -qua1) + "x");
                                        if (q2 != qua2)
                                            sw.WriteLine("Pen " + (q2 - qua2) + "x");
                                        if (q3 != qua3)
                                            sw.WriteLine("Book " + (q3 - qua3) + "x");
                                        sum = sum + Convert.ToInt32(price);//sum of all products

                                    }
                                    Console.WriteLine("Product name: " + name);//displaying information about the product that the user added to the list
                                    Console.WriteLine("Price of this product: " + price);
                                    Console.WriteLine("Total now: " + sum);
                                    Console.WriteLine("Do you want to add another product ?");
                                    answer = Console.ReadLine();//decided to end the cycle in a simpler way with such a simple question that fills the end of the order
                                    if ((sum != 0) && (answer != "yes"))
                                    {
                                        sw.WriteLine("Total: " + sum);
                                        Console.WriteLine("Your order saved");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("This product has run out, choose another one");//wrote a script if products run out
                                    i = 0;
                                    Console.WriteLine("Do you want to add product ?(yes)");
                                    answer = Console.ReadLine();
                                    if (answer != "yes")
                                    {
                                        Console.WriteLine("The order is empty or something written wrong");
                                    }
                                }

                            }
                        }
                    }
                }
                Console.WriteLine("Do you want to create or check order ?(yes)");//this huge cycle starts the whole process anew, which makes it possible to create several more orders
                restart = Console.ReadLine();
            }
        }
    }
}//maybe I did what was not required, but I wanted to bring the program to perfection.
//Alas, I didn't have the strength to completely polish it off. I think you noticed that the project was loaded at 6 in the morning,
//I did it all night and all the past day. Can you please send me my mistakes later? I wonder where I could decide better