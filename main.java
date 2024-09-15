// Jake Sussner
// Program 1
// Converting a number from one base to another with a given bit length
// This file contains the functionality of the code outside of the Number class


// import io and util packages
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Scanner;

class main{
    public static void main(String[] args) {
        // read in the text file
        try {
            File inputFile = new File("input.txt");
            Scanner myReader = new Scanner(inputFile);
            while (myReader.hasNextLine()) {
                // Grab data from each line, initialze variables
                String data = myReader.nextLine();
                String numbers[] = data.split(" ", 0);
                long num = Long.parseLong(numbers[0]);
                long currBase = Long.parseLong(numbers[1]);
                long nextBase = Long.parseLong(numbers[2]);
                long numBits = Long.parseLong(numbers[3]);

                // create number object
                Number number = new Number(num, currBase, nextBase, numBits);
                
                // Declare and write finalNum to output file
                String finalNum = "";
                try(FileWriter myWriter = new FileWriter("output.txt", true)){
                    if (currBase == 10) {
                        finalNum = number.fromBaseTen(num);
                    } else if (nextBase == 10) {
                        finalNum = String.valueOf(number.toBaseTen(true));
                    } else {
                        finalNum = number.convertToOtherBase();
                    }
                    myWriter.write(finalNum + "\n");
                } catch (IOException e) {
                    System.out.println("An error occured.");
                    e.printStackTrace();
                }
            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occured.");
            e.printStackTrace();
        }
    }
}