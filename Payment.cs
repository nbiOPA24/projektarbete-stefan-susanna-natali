using System.Data;

public class Receipt
{
    public int ReceiptNumber { get; set; }
    public static int nextReceiptNumber = Data.LoadNextReceiptNumber("nextreceiptnumber.json");
    public double PaidAmount { get; set; }
    public double Tips { get; set; }
    public double AmountToPay { get; set; }
    public double Vat12 { get; set; }
    public double Vat25 { get; set; }
    public double Netto { get; set; }
    public bool IsCash { get; set; }
    public DateTime PaymentAccepted { get; set; }
    public int CurrentTable { get; set; }
    public string? CurrentFirstName { get; set; }
    public int CurrentUserId { get; set; }
    public List<Product> paidProductList { get; private set; }

    public Receipt(Receipt receipt, int currentTable, User user)

    {
        PaidAmount = receipt.PaidAmount;
        Tips = receipt.Tips;
        AmountToPay = receipt.AmountToPay;
        Vat12 = receipt.Vat12;
        Vat25 = receipt.Vat25;
        Netto = receipt.Netto;
        IsCash = receipt.IsCash;
        PaymentAccepted = receipt.PaymentAccepted;

        CurrentTable = currentTable;
        CurrentFirstName = user.FirstName;
        CurrentUserId = user.UserId;
        ReceiptNumber = nextReceiptNumber;
        nextReceiptNumber++;
        paidProductList = new List<Product>();

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
    public static void PrintReceipt(Receipt receipt)
    {
        Data.LoadNextReceiptNumber("nextreceiptnumber.json");

        receipt.PaymentAccepted = DateTime.Now;
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);
        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + Receipt.nextReceiptNumber); //SÅKLART nextNumber ska skrivas ut och inte receiptnumber. för nextNumber är static/för nextnumber inte sätts i konstruktorn?
        Console.WriteLine("Bordsnummer: #" + receipt.CurrentTable);
        Console.WriteLine("Användare: " + currentUser.FirstName + " - " + currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + receipt.PaymentAccepted); //TODO DateTime från när betalningen gått igenom
        Console.WriteLine("Beställda artiklar: ");
        UserInterFace.PrintOrderlist();
        PrintPaidProductList(receipt);
        Console.WriteLine("------------------------");
        Console.WriteLine("Betald summa: " + Math.Round(receipt.PaidAmount, 2));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(receipt.Tips, 2));//Varav dricks(Extra)
        receipt.Netto = receipt.AmountToPay - receipt.Vat12 - receipt.Vat25;

        Console.WriteLine("Netto: " + Math.Round(receipt.Netto, 2));
        Console.WriteLine("Varav moms 12%: " + Math.Round(receipt.Vat12, 2));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(receipt.Vat25, 2));
        Console.WriteLine("------------------------");
        Console.WriteLine();
        receipt = new(receipt, TableHandler.CurrentTable, currentUser); //receipt.ReceiptNumber, receipt.PaidAmount, receipt.Tips, receipt.AmountToPay, receipt.Vat12, receipt.Vat25, receipt.Netto, receipt.IsCash, receipt.PaymentAccepted, TableHandler.CurrentTable, currentUser.FirstName, currentUser.UserId);// nu behövs inte detta: ReceiptNumber, receipt.PaidAmount, receipt.Tips, receipt.AmountToPay, receipt.Vat12, receipt.Vat25, receipt.Netto, receipt.IsCash, receipt.PaymentAccepted, TableHandler.CurrentTable, currentUser.FirstName, currentUser.UserId
        receiptList.Add(receipt);
        foreach (Product p in UserInterFace.orderList)
        {
            receipt.paidProductList.Add(p);
        }
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
    public static double CalculateVat(Receipt receipt, out double totalVat12, out double totalVat25)
    {
        totalVat12 = 0;
        totalVat25 = 0;
        foreach (Product p in UserInterFace.orderList)
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
        return totalVat12;
        return totalVat25;

    }
    #endregion
    #region StartPayment
    public static void StartPayment(Receipt receipt)
    {
        Data.LoadNextReceiptNumber("receiptnumber.json");

        if (receipt.AmountToPay > 0)
        {

            Console.WriteLine("*******BETALNING********");
            Console.Write("(K)ort eller (C)ash?: ");
            string? input = Console.ReadLine().ToUpper();
            double totalSum = UserInterFace.CountTotal(receipt);
            Console.WriteLine("Totalbelopp: " + totalSum);
            switch (input)
            {

                case "K": // KORT
                    receipt.IsCash = false;
                    GetPayment(receipt);
                    break;


                case "C": // KONTANT
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
    #endregion
    #region GetPayment
    public static void GetPayment(Receipt receipt) // Product product, Table table 
    {
        while (true)
        {
            double amountToPay = UserInterFace.CountTotal(receipt);
            Console.Write("Slå in totalbelopp ink. ev. dricks: ");
            receipt.PaidAmount = int.Parse(Console.ReadLine());
            receipt.Tips = receipt.PaidAmount - amountToPay;

            if (receipt.PaidAmount < amountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + amountToPay);
                continue;
            }
            if (receipt.IsCash == true)
            {
                Console.Write("Slå in mottagna pengar: ");
                double givenMoney = int.Parse(Console.ReadLine());
                double change = givenMoney - receipt.PaidAmount; //varför grått? ska detta visa pengar tillbaka? Ska man det?
                Console.WriteLine("Tack!");
                Thread.Sleep(1000);
                PrintReceipt(receipt);
                // foreach (Product p in UserInterFace.orderList)
                // {
                //     receipt.paidProductList.Add(p);
                // }
                break;
            }
            else if (receipt.PaidAmount >= amountToPay)
            {
                Console.WriteLine("Betalning genomförs");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                Console.WriteLine(" Tack!");
                Thread.Sleep(1000);
                Data.LoadNextReceiptNumber("receiptnumber.json");
                receipt.AmountToPay = amountToPay;
                double totalVat = CalculateVat(receipt, out double vat12, out double vat25);
                receipt.Vat12 = vat12;
                receipt.Vat25 = vat25;
                PrintReceipt(receipt); //TODO indata kvittonummer för att hålla reda på?
                ReportHandler.GetSoldProducts();

                // foreach (Product p in UserInterFace.orderList)
                // {
                //     receipt.paidProductList.Add(p);
                // }
                UserInterFace.orderList.Clear(); //clear eller = null.

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
