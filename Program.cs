using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny
{
    class Program
    {

        
        
            static void Main()
            {
                string code = @"
/*Sample program includes all 30 rules*/
int sum(int a, int b)
{
    return a + b;
}
int main()
{
int val, counter;
read val;
counter:=0;                                                                                
repeat                                                                                
val := val - 1;
write ""Iteration number ["";
write counter;
write ""] the value of x = "";
write val;
write endl;                          
counter := counter+1;                                                      
until val = 1                                                                                  
write endl;                                                                                
string s := ""number of Iterations = "";
write s;                                                                                
counter:=counter-1;
write counter;
/* complicated equation */    
float z1 := 3*2*(2+1)/2-5.3;
z1 := z1 + sum(1,y);
if  z1 > 5 || z1 < counter && z1 = 1 then 
write z1;
elseif z1 < 5 then
    z1 := 5;
else
    z1 := counter;
end
return 0;
}
";

                Scanner scanner = new Scanner();
                scanner.StartScanning(code);

                foreach (var token in scanner.Tokens)
                {
                    Console.WriteLine(token.token_type + " -> " + token.lex);
                }

                Console.ReadKey();
            }
        }
    }
