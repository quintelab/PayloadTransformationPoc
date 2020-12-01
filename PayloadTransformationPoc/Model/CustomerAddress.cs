namespace PayloadTransformationPoc.Model
{
    public class CustomerAddress
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Province { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ZipCode { get; set; }
        public int IsPoBox { get; set; }
    }
}
