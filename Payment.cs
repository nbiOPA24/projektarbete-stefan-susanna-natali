using System.Data;

public class Receipt
{
    public int ReceiptNumber { get; set; }
    public static int nextReceiptNumber = Data.LoadNextReceiptNumber("nextreceiptnumber.id");
    public double PaidAmount { get; set; }
    public double Tips { get; set; }
    public double AmountToPay { get; set; }
    public double Vat12 { get; set; }
    public double Vat25 { get; set; }
    public double Netto { get; set; }
    public bool IsCash { get; set; }
    public DateTime PaymentAccepted { get; set; }
    public int? CurrentTable { get; set; }
    public string? CurrentFirstName { get; set; }
    public int CurrentUserId { get; set; }
    public List<Product> paidProductList { get; private set; }

    public Receipt(Order order, int? currentTable, bool isCash, double paidAmount, double tips)
    //
    {
        PaidAmount = paidAmount;
        Tips = tips;

        // Vat12 = order.ProductList[1].VatItem;
        // Vat25 = order.Vat25;
        // foreach (Product p in order.ProductList)
        // {
        //     if (p.VatItem == Product.VatRate._12)
        //     {
        //         Vat12 = p.Price * 0.12;
        //     }
        //     else if (p.VatItem == Product.VatRate._25)
        //     {
        //         Vat25 = p.Price * 0.25;
        //     }
            

        // }
        double amountToPay = UserInterFace.CountTotal(order);
        Payment.CalculateVat(order, out double vat12, out double vat25);
        Netto = amountToPay - vat12 - vat25;
        IsCash = isCash;
        PaymentAccepted = DateTime.Now;
        AmountToPay = amountToPay;

        CurrentTable = currentTable;
        CurrentFirstName = order.User.FirstName;
        CurrentUserId = order.User.UserId;
        ReceiptNumber = nextReceiptNumber;
        nextReceiptNumber++;
        paidProductList = order.ProductList;//new List<Product>();

    }
    public Receipt()
    {
        paidProductList = new List<Product>();
    }


}
public static class Payment
{


    public static List<Receipt> receiptList = new();
    #region PrintReceipt
    public static void PrintReceipt(Order order, Receipt receipt) //Order order, Table table, Receipt receipt)
    {
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);
        Data.LoadNextReceiptNumber("nextreceiptnumber.json");
        //Receipt receipt = new(order);
        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + Receipt.nextReceiptNumber); //SÅKLART nextNumber ska skrivas ut och inte receiptnumber. för nextNumber är static/för nextnumber inte sätts i konstruktorn?
        Console.WriteLine("Bordsnummer: #" + TableHandler.CurrentTable);
        Console.WriteLine("Användare: " + currentUser.FirstName + " - " + currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + receipt.PaymentAccepted); //TODO DateTime från när betalningen gått igenom
        Console.WriteLine("Beställda artiklar: ");
        UserInterFace.PrintOrderlist(order);
        PrintPaidProductList(receipt);
        Console.WriteLine("------------------------");
        Console.WriteLine("Betald summa: " + Math.Round(receipt.PaidAmount, 2));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(receipt.Tips, 2));//Varav dricks(Extra)
        receipt.Netto = receipt.AmountToPay - receipt.Vat12 - receipt.Vat25;

        Console.WriteLine("Netto: " + Math.Round(receipt.Netto, 2));
        Console.WriteLine("Varav moms 12%: " + Math.Round(receipt.Vat12, 2));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(receipt.Vat25, 2));
        //receipt = new(receipt,); //receipt.ReceiptNumber, receipt.PaidAmount, receipt.Tips, receipt.AmountToPay, receipt.Vat12, receipt.Vat25, receipt.Netto, receipt.IsCash, receipt.PaymentAccepted, TableHandler.CurrentTable, currentUser.FirstName, currentUser.UserId);// nu behövs inte detta: ReceiptNumber, receipt.PaidAmount, receipt.Tips, receipt.AmountToPay, receipt.Vat12, receipt.Vat25, receipt.Netto, receipt.IsCash, receipt.PaymentAccepted, TableHandler.CurrentTable, currentUser.FirstName, currentUser.UserId
        receiptList.Add(receipt);
        // foreach (Product p in UserInterFace.orderList)
        // {
        //     receipt.paidProductList.Add(p);
        // }
        Data.SaveReceiptList("receipt.json");
        Data.SaveNextReceiptNumber("nextreceiptnumber.json");
    }
    #endregion
    #region ReceiptList
    public static void PrintReceiptList(Receipt receipt)
    {
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);
        foreach (Receipt r in receiptList)
        {
            Console.WriteLine("\t\tRestaurangNamn");
            Console.WriteLine("\t\tKvittonummer: " + r.ReceiptNumber);
            Console.WriteLine("Bordsnummer: #" + r.CurrentTable);
            Console.WriteLine("Användare: " + r.CurrentFirstName + " - " + r.CurrentUserId); //Vilken Användare/servis
            Console.WriteLine("Datum: " + r.PaymentAccepted);
            Console.WriteLine("Beställda artiklar: ");
            Console.WriteLine("Artiklar från paidproductlist: ");
            PrintPaidProductList(receipt);
            Console.WriteLine("------------------------");
            Console.WriteLine("Betald summa: " + Math.Round(r.PaidAmount, 3));//Betald summa
            Console.WriteLine("Varav dricks: " + Math.Round(r.Tips, 2));//Varav dricks(Extra)
            Console.WriteLine("Netto: " + Math.Round(r.Netto, 2));
            Console.WriteLine("Varav moms 12%: " + Math.Round(r.Vat12, 2));//Varav moms
            Console.WriteLine("Varav moms 25%: " + Math.Round(r.Vat25, 2));
        }

    }
    public static void PrintPaidProductList(Receipt receipt)
    {
        if (receipt.paidProductList == null)
        {
            Console.WriteLine("-----TOM-----");
        }
        else if (receipt.paidProductList.Count == 0)
        {
            Console.WriteLine("-----0.TOM.0-----");
        }
        foreach (Product p in receipt.paidProductList)
        {

            Console.WriteLine(p.ProductNumber + ". " + p.Name + " - " + p.Price + " kr. ");
        }

    }
    #endregion
    #region CalculateVat
    public static void CalculateVat(Order order, out double totalVat12, out double totalVat25)
    {
        totalVat12 = 0;
        totalVat25 = 0;
        foreach (Product p in order.ProductList)
        {
            if (p.MenuItem == Product.ProductType.Food || p.MenuItem == Product.ProductType.Beverage)
            {
                totalVat12 += p.Price;
                totalVat12 = totalVat12 * 0.12;
            }

            else if (p.MenuItem == Product.ProductType.Alcohol)
            {
                totalVat25 += p.Price;
                totalVat25 = totalVat25 * 0.25;
            }
        }
        //return totalVat12;

    }
    #endregion
    #region StartPayment
    public static void StartPayment(Order order, int? table)//Table table, Receipt receipt)
    {
        double totalSum = UserInterFace.CountTotal(order);
        //Data.LoadNextReceiptNumber("receiptnumber.json");
        //Receipt receipt = new();

        if (totalSum > 0)
        {
            bool isCash;
            Console.WriteLine("*******BETALNING********");
            Console.Write("(K)ort eller (C)ash?: ");
            string? input = Console.ReadLine().ToUpper();
            Console.WriteLine("Totalbelopp: " + totalSum);
            switch (input)
            {

                case "K": // KORT
                    isCash = false;
                    GetPayment(order, table, isCash);
                    break;


                case "C": // KONTANT
                    isCash = true;
                    GetPayment(order, table, isCash);
                    break;
            }
        }
        else
        {
            Console.WriteLine("Finns inga produkter att ta betalt för!");
        }

    }
    #endregion
    #region GetPayment
    public static void GetPayment(Order order, int? table, bool isCash) // Product product, Table table 
    {
        while (true)
        {
            Console.Write("Slå in totalbelopp ink. ev. dricks: ");
            double paidAmount = double.Parse(Console.ReadLine());
            double amountToPay = UserInterFace.CountTotal(order);
            double tips = paidAmount - amountToPay;
            Receipt receipt = new(order, table, isCash, paidAmount, tips);
            if (paidAmount < amountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + amountToPay);
                continue;
            }
            if (isCash == true)
            {
                Console.Write("Slå in mottagna pengar: ");
                double givenMoney = double.Parse(Console.ReadLine());
                double change = givenMoney - paidAmount;
                Console.WriteLine("Tack!");
                Thread.Sleep(1000);

                PrintReceipt(order, receipt);
                // foreach (Product p in order.ProductList)
                // {
                //     receipt.paidProductList.Add(p);
                // }
                break;
            }
            else if (paidAmount >= amountToPay)
            {
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
                Data.LoadNextReceiptNumber("receiptnumber.json");

                PrintReceipt(order, receipt); //TODO indata kvittonummer för att hålla reda på?
                //ReportHandler.GetSoldProducts();

                // foreach (Product p in UserInterFace.orderList)
                // {
                //     receipt.paidProductList.Add(p);
                // }
                //UserInterFace.orderList.Clear();
                //receipt.paidProductList.Clear();

                break;
            }
            else
            {
                Console.WriteLine("Ogilig inmatning!");
            }
        }
    }
    #endregion

}
