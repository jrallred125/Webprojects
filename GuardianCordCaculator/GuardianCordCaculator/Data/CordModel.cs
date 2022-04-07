namespace GuardianCordCaculator.Data
{
    public class CordModel
    {
        public int XCord { get; set; }
        public int ZCord { get; set; }

        public CordModel(int xCord, int zCord)
        {
            XCord = xCord;
            ZCord = zCord;  
        }

        public override string ToString()
        {
            return $"({XCord},{ZCord})";
        }
    }
}
