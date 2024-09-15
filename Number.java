// Jake Sussner
// Program 1
// Number Class

// import util packages
import java.util.HashMap;
import java.util.ArrayList;
import java.util.Collections;

// Create class
public class Number {
    private long number;
    private long currBase;
    private long nextBase;
    private long numBits;

    // constructor
    Number (long number, long currBase, long nextBase, long numBits){
        this.number = number;
        this.currBase = currBase;
        this.nextBase = nextBase;
        this.numBits = numBits;
    }

    // getters
    public long getNumber() {
        return this.number;
    }
    public long getCurrBase() {
        return this.currBase;
    }
    public long getNextBase() {
        return this.nextBase;
    }
    public long getNumBits() {
        return this.numBits;
    }

    // methods

    // check if the current conversion meets bit length requirements
    public String checkBits(String newNum) {
    // if the number exceeds the allowed bits
        if (newNum.length() > this.numBits) {
            return "OVERFLOW";
        } else if (newNum.length() < this.numBits){
            // if the number is smaller than the required number of bits, pad with leading zeros
            StringBuilder zeros = new StringBuilder();
            int length = (int) (this.numBits) - newNum.length();
            for (int i = 0; i < length; i++){
                zeros.append("0");
            }
            String finalNum = String.valueOf(zeros) + newNum;
            return finalNum;
        } else {
            return newNum;
        }
    }

    // convert a number from base ten to any other base up to base 16
    public String fromBaseTen(long numberToConvert){
        String newNum = "";

        // if our new base is less than or equal to 10, we don't have to worry about using letters
        if (this.nextBase <= 10){
            ArrayList<String> remainders = new ArrayList<>();
            while (numberToConvert > 0){
                // Use the method of integer division and remainders to convert
                long temp = numberToConvert % this.nextBase;
                remainders.add(String.valueOf(temp));
                numberToConvert = numberToConvert / this.nextBase;
            }
            // reverse remainders arraylist to get final conversion number
            Collections.reverse(remainders);

            StringBuilder sb = new StringBuilder();
            for (String remainder : remainders) {
                sb.append(remainder);
            }

            // store conversion
            newNum = sb.toString();
        
        // dealing with a base larger than 10
        } else {
            // create a hashmap to handle letter conversions
            HashMap<Integer, String> numMap = new HashMap<>();
            numMap.put(10, "A");
            numMap.put(11, "B");
            numMap.put(12, "C");
            numMap.put(13, "D");
            numMap.put(14, "E");
            numMap.put(15, "F");

            ArrayList<String> remainders = new ArrayList<>();
            while (numberToConvert > 0) {
                long remainder = numberToConvert % this.nextBase;

                // use the map for remainders > 9
                if (remainder > 9) {
                    remainders.add(numMap.get((int) remainder));
                } else {
                    remainders.add(String.valueOf(remainder));
                }

                numberToConvert = numberToConvert / this.nextBase;
            }

            // reverse the list for correct order**
            Collections.reverse(remainders);

            // build the new number as a string
            StringBuilder sb = new StringBuilder();
            for (String remainder : remainders) {
                sb.append(remainder);
            }

            // store conversion
            newNum = sb.toString();
        }
        // check if bit length is accurate
        return this.checkBits(newNum);
    }
    
    // to convert a number to base ten
    public String toBaseTen(boolean flag){
        // hashmap to handle current bases that are larger than 10
        HashMap<Character, Integer> numMap = new HashMap<>();
        numMap.put('A', 10);
        numMap.put('B', 11);
        numMap.put('C', 12);
        numMap.put('D', 13);
        numMap.put('E', 14);
        numMap.put('F', 15);
        // declare variables
        long newNum = 0;
        int power = 0;
        String numStr = Long.toString(this.number).toUpperCase();
        numStr = new StringBuilder(numStr).reverse().toString();
        // convert number
        for (int i = 0; i < numStr.length(); i++) {
            char num = numStr.charAt(i);
            int temp;
            if (numMap.containsKey(num)){
                temp = numMap.get(num);
            } else {
                temp = num - '0';
            }
            
            newNum += temp * Math.pow(this.currBase, power);
            power += 1;
        }
        // store conversion
        String newNumStr = String.valueOf(newNum);

        // check to see if still need to convert to other base
        // if not, check bit length
        if (flag){
            return this.checkBits(newNumStr);
        // if need to still convert to other base, do not check bit length
        } else {
            return newNumStr;
        }
    }

    // convert a number from a non base ten to another non base ten
    public String convertToOtherBase(){
        String baseTenNum = this.toBaseTen(false);
        return this.fromBaseTen(Long.parseLong(baseTenNum));
    }

    // overide
    public String toString(){
        return String.format("Number: %d, Current Base: %d, Next Base: %d, Number of Bits: %d \n", this.number, this.currBase, this.nextBase, this.numBits);
    }
}
