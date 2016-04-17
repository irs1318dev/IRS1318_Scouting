
public class Main
{
	public static void main(String[] args)
	{
		MainData mainData = new MainData();
        if(args.length > 0) mainData.connect(args[0]);
        else mainData.run();
	}
}
