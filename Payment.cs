public class Receipt
{
    public int ReceiptNumber {get;set;}
    public double PaidAmount { get; set; }
    public double Tips { get; set; }
    public double AmountToPay { get; set; }
    public double Vat12 { get; set; }
    public double Vat25 { get; set; }
    public double Netto {get;set;}
    public bool IsCash { get; set; }
    public DateTime PaymentAccepted { get; set; }

    public Receipt(int receiptNumber, double paidAmount, double tips, double amountToPay, double vat12, double vat25, double netto, bool isCash, DateTime paymentAccepted)
    {
        ReceiptNumber = receiptNumber;
        PaidAmount = paidAmount;
        Tips = tips;
        AmountToPay = amountToPay;
        Vat12 = vat12;
        Vat25 = vat25;
        Netto = netto;
        IsCash = isCash;
        PaymentAccepted = paymentAccepted;
    }


}
public static class Payment
{
    
    public static List<Receipt> receiptList = new();
        
    public static void PrintReceiptList ()
    {
        foreach (Receipt r in receiptList)
        {
        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + r.ReceiptNumber);
        //if (blabla tablenr != null)

        Console.WriteLine("Bordsnummer: #");
        // Console.WriteLine("Användare: " + r.UserHandler.currentUser.FirstName + " - " + r.currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + r.PaymentAccepted); //TODO DateTime från när betalningen gått igenom
        Console.WriteLine("Beställda artiklar: ");
        UserInterFace.PrintOrderlist(); //TODO Skriva ut lista från rätt bord
        Console.WriteLine("------------------------");
        Console.WriteLine("Betald summa: " + Math.Round(r.PaidAmount,3));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(r.Tips));//Varav dricks(Extra)
        Console.WriteLine("Netto: " + Math.Round(r.Netto));
        Console.WriteLine("Varav moms 12%: " + Math.Round(r.Vat12));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(r.Vat25));
        }
    }
    public static void StartPayment(Table table, Receipt receipt)
    {

        if (receipt.AmountToPay > 0)
        {
            Console.WriteLine("*******BETALNING********");
            Console.Write("1. Kort eller 2. kontant?: ");
            int input = int.Parse(Console.ReadLine());
            UserInterFace.CountTotal(table, receipt);
            switch (input)
            {

                case 1: // KORT
                    receipt.IsCash = false;
                    GetPayment(receipt);
                    break;


                case 2: // KONTANT
                    receipt.IsCash = true;
                    GetPayment(receipt);
                    break;
            }
        }
        else
        {
            Console.WriteLine("Finns inga produkter att ta betalt för!");
        }

    }
    public static void GetPayment(Receipt receipt) // Payment är META nu. vi ska inte betala och dricksa
    {


        while (true)
        {
            Console.Write("Slå in totalbelopp ink. ev. dricks: ");
            receipt.PaidAmount = int.Parse(Console.ReadLine());
            receipt.Tips = receipt.PaidAmount - receipt.AmountToPay;

            if (receipt.PaidAmount < receipt.AmountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + receipt.AmountToPay);
                continue;
            }
            if (receipt.IsCash == true)
            {
                Console.Write("Slå in mottagna pengar: ");
                double givenMoney = int.Parse(Console.ReadLine());
                double change = givenMoney - receipt.PaidAmount;
                Console.WriteLine("Du dricksade " + receipt.Tips + " kr."); // användaren är servitör och ska inte pay eller tip
                Console.WriteLine("Din växel är " + change + " kr.");
                Console.WriteLine("Tack!");
                Thread.Sleep(1000);
                PrintReceipt(receipt);
                break;
            }
            else if (receipt.PaidAmount >= receipt.AmountToPay)
            {
                Console.WriteLine("Du dricksade " + receipt.Tips + " kr.");
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
                PrintReceipt(receipt); //TODO indata kvittonummer för att hålla reda på? 
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
    public static void PrintReceipt(Receipt receipt) //TODO kvittokopia, kolla riktigt kvitto å se va som ska va mäd! Kuna spara kvitto. söka upp gamla kvitton.
    {
        receipt.PaymentAccepted = DateTime.Now;
        receipt.ReceiptNumber = 1000; //Todo gör till egenskap

        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + receipt.ReceiptNumber);
        //if (blabla tablenr != null)

        Console.WriteLine("Bordsnummer: #");
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);
        Console.WriteLine("Användare: " + currentUser.FirstName + " - " + currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + receipt.PaymentAccepted); //TODO DateTime från när betalningen gått igenom

        Console.WriteLine("Beställda artiklar: ");
        UserInterFace.PrintOrderlist();
        Console.WriteLine("------------------------");

        Console.WriteLine("Betald summa: " + Math.Round(receipt.PaidAmount, 3));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(receipt.Tips, 3));//Varav dricks(Extra)
        CalculateVat(receipt);
        receipt.Netto = receipt.AmountToPay - receipt.Vat12 - receipt.Vat25;
        Console.WriteLine("Netto: " + Math.Round(receipt.Netto, 2));
        Console.WriteLine("Varav moms 12%: " + Math.Round(receipt.Vat12, 3));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(receipt.Vat25, 3));
        //ReportHandler.AddToReport(ReceiptNumber, currentUser, )
        Receipt newReceipt = new(receipt.ReceiptNumber,receipt.PaidAmount,receipt.Tips,receipt.AmountToPay,receipt.Vat12,receipt.Vat25,receipt.Netto,receipt.IsCash,receipt.PaymentAccepted);
        receiptList.Add(newReceipt);
        //TODO lägg in allt detta i en rapportlista
        // placeholder kortuppgifter
    }

    public static void CalculateVat(Receipt receipt)
    {
        double CalcVat12 = 0;
        double CalcVat25 = 0;
        foreach (Product p in UserInterFace.orderList)
        {
            if (p.MenuItem == Product.ProductType.Food || p.MenuItem == Product.ProductType.Beverage)
            {

                CalcVat12 += p.Price;
                receipt.Vat12 = CalcVat12 * 0.12;

            }

            else if (p.MenuItem == Product.ProductType.Alcohol)
            {
                CalcVat25 += p.Price;
                receipt.Vat25 = CalcVat25 * 0.25;
            }
        }

    }
}