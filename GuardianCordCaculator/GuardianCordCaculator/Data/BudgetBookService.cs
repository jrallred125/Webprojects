namespace GuardianCordCaculator.Data
{
    public class BudgetBookService
    {
        private static List<List<CordModel>> Cords = new();

        public async Task<bool>CaculateCords(CordModel cord)
        {
            int[] offset = new int[5] {2,13,29,45,55 };
            for (int row = 0; row < offset.Length; row++)
            {
                Cords.Add(new List<CordModel>());
                for (int col = 0; col < offset.Length; col++)
                {
                    Cords[row].Add( new(cord.XCord + offset[row], cord.ZCord + offset[col]));
                }
            }
            return true;
        }

        public async Task<List<List<CordModel>>> GetSpawnCords()
        {
            return Cords;
        }


    }
}
