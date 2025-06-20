namespace Code_tests;

[TestClass]
public class recordTests
{
    [TestMethod]
    public void Records()
    {
        (int a, int b) record = (1, 2);
        (int c, int d) record2 = (3, 5);
        record2 = (4, 6);
        record = record2;


    }
}
