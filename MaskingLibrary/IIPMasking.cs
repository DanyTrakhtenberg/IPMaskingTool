namespace MaskingLibrary
{
    public interface IIPMasking
    {
        string MaskIP(string ip, int[] maskArr);
    }
}