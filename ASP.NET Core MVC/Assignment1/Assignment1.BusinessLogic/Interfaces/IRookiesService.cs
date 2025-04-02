namespace Assignment1.BusinessLogic.Interfaces
{
    public interface IRookiesService
    {
        string GetMales();
        string GetOldestRookie();
        string GetFullnames();
        string GetRookiesBornIn(int year);
        string GetRookiesBornAfter(int year);
        string GetRookiesBornBefore(int year);
        Stream GetExcel();
    }
}
