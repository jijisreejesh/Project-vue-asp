public class Sales{
    public int Id{get;set;}
    public int Customer_Id{get;set;}
    public int Product_Id{get;set;}
    public int Quantity{get;set;}
    public int Total_Price{get;set;}
    public DateTime Sales_Date{get;set;}
    public string Payment_Method{get;set;}
    public string Status{get;set;}
}

public class SalesViewModel : Sales {
    public string CustomerName { get; set; }
}