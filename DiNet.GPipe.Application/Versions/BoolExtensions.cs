namespace DiNet.GPipe.Application.Versions;

public static class BoolExtensions 
{
    extension(bool state)
    {
        public int ToInt()
        {
            return state ? 1 : 0;
        }
    }    
}

