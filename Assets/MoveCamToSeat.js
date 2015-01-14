#pragma strict

public var seatWidth = 1;
public var currentRow:String = "a";
public var currentNumber:int = 9;

private var sec1:Section;
private var sec2:Section;


function Start () {
	sec1=new Section();
  	sec1.startPoint = [0,0,-10 ];
 	sec1.endPoint = [0,0,-50 ];
 	sec1.rows = 9;

 	sec2 = new Section();
 	sec2.startPoint = [10,0,-10 ];
 	sec2.endPoint = [20,0,-50];
 	sec2.rows = 9;

	moveToSeat(currentRow,currentNumber);
}

function Update () {
}

function parseAndMove(input:String){
	var inputArray = input.Split(' '[0]);
	var row = inputArray[0];
	var number:int = int.Parse(inputArray[1]);

	moveToSeat(row,number);

}


function moveToSeat(row:String, number:int){
	var point = findSeat(row, number);
	/*
	gameObject.transform.localPosition.x = point[0];
	gameObject.transform.localPosition.y = point[1];
	gameObject.transform.localPosition.z = point[2];
	*/
	gameObject.transform.parent.localPosition.x = point[0];
	gameObject.transform.parent.localPosition.y = point[1];
	gameObject.transform.parent.localPosition.z = point[2];

	Debug.Log("Moved to: "+row+" "+number+" | "+point[0]+", "+point[1]+", "+point[2]+", ");

}

function findSeat(rowString:String, number:int){
	var point = [0,0,0];
	var thisSection:Section = new Section();
	var row:float = letterToInt(rowString);

	if(number<50){
		thisSection = sec1;
	} else if (number>100){
		thisSection = sec2;
		number -= 100;
	}

	var reverse = 1;

	point[1] = Mathf.Lerp(thisSection.startPoint[1], thisSection.endPoint[1], ( row ) / thisSection.rows  );
  	point[0] = Mathf.Lerp(thisSection.startPoint[0], thisSection.endPoint[0], ( row ) / thisSection.rows  ) + (number* seatWidth *reverse )  ;
  	point[2] = Mathf.Lerp(thisSection.startPoint[2], thisSection.endPoint[2], ( row ) / thisSection.rows );

	return point;
}


function letterToInt(row:String){
	var rowInt:int;
	rowInt = row[0];
	rowInt -= 97;

	Debug.Log("int calc: " + rowInt);

	return rowInt;
	/*
  if (row.length < 2){
	var rowLetter:int = row[0]  ;
    if (rowLetter<91){
      return rowLetter - 65;
    } else if ( rowLetter > 90 ){
      return rowLetter - 97;
    } else{
      Debug.Log("invalid row! input: " + row[0]);
    }
    
   } else if (row.length > 2){
   	

   
   }
   */

  return 0;

}

class Section{
	var startPoint = [0,0,0];
	var endPoint = [0,0,0];

	var rows:int;
	var seatMin;
	var seatMax;
	var isLeftToRight = true;





}