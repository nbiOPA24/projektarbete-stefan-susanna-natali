public static class Payment
{

    public static double PaidAmount { get; set; }
    public static double Tips { get; set; }
    public static double AmountToPay { get; set; }
    public static double Vat12 { get; set; }
    public static double Vat25 { get; set; }
    public static bool IsCash { get; set; }
    public static DateTime PaymentAccepted { get; set; }
    public static void StartPayment(Table table)
    {

        if (AmountToPay > 0)
        {
            Console.WriteLine("*******BETALNING********");
            Console.Write("1. Kort eller 2. kontant?: ");
            int input = int.Parse(Console.ReadLine());
            UserInterFace.CountTotal(table);
            switch (input)
            {

                case 1: // KORT
                    IsCash = false;
                    GetPayment();
                    break;


                case 2: // KONTANT
                    IsCash = true;
                    GetPayment();
                    break;
            }
        }
        else
        {
            Console.WriteLine("Finns inga produkter att ta betalt för!");
        }

    }
    public static void GetPayment() // Payment är META nu. vi ska inte betala och dricksa
    {


        while (true)
        {
            Console.Write("Slå in totalbelopp ink. ev. dricks: ");
            PaidAmount = int.Parse(Console.ReadLine());
            Tips = PaidAmount - AmountToPay;

            if (PaidAmount < AmountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + AmountToPay);
                continue;
            }
            if (IsCash == true)
            {
                Console.Write("Slå in mottagna pengar: ");
                double givenMoney = int.Parse(Console.ReadLine());
                double change = givenMoney - PaidAmount;
                Console.WriteLine("Du dricksade " + Tips + " kr."); // användaren är servitör och ska inte pay eller tip
                Console.WriteLine("Din växel är " + change + " kr.");
                Console.WriteLine("Tack!");
                Thread.Sleep(1000);
                PrintReceipt();
                break;
            }
            else if (PaidAmount >= AmountToPay)
            {
                Console.WriteLine("Du dricksade " + Tips + " kr.");
                Console.WriteLine("Betalning genomförs");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.WriteLine(" Tack!");
                Thread.Sleep(1000);
                PrintReceipt(); //TODO indata kvittonummer för att hålla reda på? 
                UserInterFace.orderList.Clear();

                break;
            }
            else
            {
                Console.WriteLine("Ogilig inmatning!");
            }
        }
    }
    public static void SplitPayment() // vänta med denna
    {
        // Splitta broderligt
        // Splitta per person

    }
    public static void PrintReceipt() //TODO kvittokopia, kolla riktigt kvitto å se va som ska va mäd! Kuna spara kvitto. söka upp gamla kvitton.
    {
        PaymentAccepted = DateTime.Now;
        int ReceiptNumber = 1000; //Todo gör till egenskap

        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + ReceiptNumber);
        //if (blabla tablenr != null)
        Console.WriteLine("Bordsnummer: #");
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);
        Console.WriteLine("Användare: " + currentUser.FirstName + " - " + currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + PaymentAccepted); //TODO DateTime från när betalningen gått igenom
        Console.WriteLine("Beställda artiklar: ");
        UserInterFace.PrintOrderlist();
        Console.WriteLine("------------------------");
        Console.WriteLine("Betald summa: " + Math.Round(PaidAmount, 3));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(Tips, 3));//Varav dricks(Extra)
        CalculateVat();
        double Netto = AmountToPay - Vat12 - Vat25;
        Console.WriteLine("Netto: " + Math.Round(Netto, 2));
        Console.WriteLine("Varav moms 12%: " + Math.Round(Vat12, 3));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(Vat25, 3));
        //TODO lägg in allt detta i en rapportlista
        // placeholder kortuppgifter
    }

    public static void CalculateVat()
    {
        double CalcVat12 = 0;
        double CalcVat25 = 0;
        foreach (Product p in UserInterFace.orderList)
        {
            if (p.MenuItem == Product.ProductType.Food || p.MenuItem == Product.ProductType.Beverage)
            {

                CalcVat12 += p.Price;
                Vat12 = CalcVat12 * 0.12;

            }

            else if (p.MenuItem == Product.ProductType.Alcohol)
            {
                CalcVat25 += p.Price;
                Vat25 = CalcVat25 * 0.25;
            }
        }

    }
}