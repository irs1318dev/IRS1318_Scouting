import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class Main
{
	public static void main(String[] args)
	{
		File file = new File("Matches.csv");
		try {
			file.createNewFile();
			FileWriter fileWriter = new FileWriter(file);
			fileWriter.write("Match,Team,Side,\n");
			fileWriter.close();
		}catch (IOException e) {}
		MainData mainData = new MainData();
		mainData.run(true);
	}
}
