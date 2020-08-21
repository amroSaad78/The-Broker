namespace WebMVC.Model
{
    public class GenricApartment<Tin1, Tin2, Tin3, Tin4>
    {
        public GenricApartment()
        {

        }
        public GenricApartment(Tin1 in1, Tin2 in2, Tin3 in3, Tin4 in4)
        {
            In1 = in1;
            In2 = in2;
            In3 = in3;
            In4 = in4;
        }

        public Tin1 In1 { get; set; }
        public Tin2 In2 { get; set; }        
        public Tin3 In3 { get; set; }        
        public Tin4 In4 { get; set; }        
    }
}
