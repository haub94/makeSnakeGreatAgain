makeSnakeGreatAgain
Date: 13.06.2022 (update getter and setter)

###code-style
- blockcode (dont use spaghetticode)
- camelCase-Notation (myFirstFuction(); variableOne; dogCatMouseWhatEver) 
- 120 characters per line
- language = english!!!

##scriptheader-style (last * and / == character nr 120):
/**************************************************************************************************
Name:          scriptXyz.cs
Description:   Descripe your function!
Author(s):     - Dev1
               - Dev2 (exampleFunction() )
Date:          jjjj-mm-dd
Version:       Vx.y 
TODO:          - Implement great stuff
               - Remove great stuff
**************************************************************************************************/

##functionheader-style
/*
 * Author(s): Name(s) from the author(s) (Haubold Markus; Rittrich Daniel)
 * Description: Describe the function in a short sentence
 * Parameter var1: Describe the use of the parameter
 * Parameter var2: Describe the use of the parameter
 * Return: Describe what the function returns (if there is no return: -)
 * Version: x.y
*/

##getter-sytle
//note: getter for private variable myPrivateVar1
//use the variable returnValue ! (if the getter gets more logic in the future, you can work with 
//the prepared variable)

public <returnType> get<MyPrivateVar1>() {  
  <returnType> returnValue;
  returnValue = myPrivateVar1;
                                //set space between your last action and the return state!
  return returnValue;
}

##setter-style
//note: setter for private variable myPrivateVar1 

public void set<MyPrivateVar1>(<setType> <yourValueName>) {  
  myPrivateVar1 = <yourValueName>;
}


##brackets and spacing:
if ((var1 * var2) == 1) {
  var1 = ((var2 / 10) * 5); //one tabulator to the right
}

bool myFunction(string text, double number) {
  //do action 1
  //do action 2
                  //set space between your last action and the return state!
  return true;
}