#pragma strict

private var ACCELEROMETER_SENSITIVITY:float =  8192.0;
private var GYROSCOPE_SENSITIVITY:float = 65.536;
 
private var M_PI:float = 3.14159265359;
 
private var dt:float = 0.01;				// 10 ms sample rate!    
 
public var heading:float;
public var pitch:float;
public var roll:float;

public var _gyrData:Vector3;	
public var _accData:Vector3;
public var _compData:Vector3;

public var gravity:Vector3;

public var currentRotation:Vector3;


function Start () {
	Input.gyro.enabled = true;
}

function Update () {

	_gyrData= Input.gyro.rotationRateUnbiased;
	gravity= Vector3.Lerp(gravity, Input.acceleration,0.1);
	_accData =  Vector3.Lerp(_accData, Input.acceleration,0.75) - gravity;
	_compData = Input.compass.rawVector;
	
	ComplementaryFilter(_accData,_gyrData, _compData);
	
	heading += currentRotation.x;
	pitch += currentRotation.y;
	roll += currentRotation.z;
	
	transform.localRotation = GetQuaternionFromHeadingPitchRoll(heading, pitch, roll);
	
	/*
	transform.localRotation.x = pitch;
	transform.localRotation.y = roll;
	transform.localRotation.z = _gyrData[2];
	*/
}

public function ComplementaryFilter(accData:Vector3 , gyroData:Vector3, compassData:Vector3){

			dt = Time.deltaTime;
			//Debug.Log(dt);

			currentRotation.x  = ( 0.8f * gyroData.x  ) + ( 0.2f  * accData.x );
			currentRotation.y  = ( 0.8f * gyroData.y  ) + ( 0.2f  * accData.y );
			currentRotation.z  = ( 0.8f * gyroData.z  ) + ( 0.2f  * accData.z );

		}


public function GetQuaternionFromHeadingPitchRoll( inputHeading:float,  inputPitch:float,  inputRoll:float) {
			var returnQuat:Quaternion = Quaternion.Euler(0f,inputHeading,0f) * Quaternion.Euler(inputPitch,0f,0f) * Quaternion.Euler(0f,0f,inputRoll);
			return returnQuat;
		}

/*
function ComplementaryFilter(accData:float[], gyrData:float[]){
    var pitchAcc:float;
    var rollAcc:float;               
 
    // Integrate the gyroscope data -> int(angularSpeed) = angle
    pitch += (gyrData[0] / GYROSCOPE_SENSITIVITY) * dt;    // Angle around the X-axis
    roll  -= (gyrData[1] / GYROSCOPE_SENSITIVITY) * dt;    // Angle around the Y-axis
 
    // Compensate for drift with accelerometer data if !bullshit
    // Sensitivity = -2 to 2 G at 16Bit -> 2G = 32768 && 0.5G = 8192
    var forceMagnitudeApprox:int = Mathf.Abs(accData[0]) + Mathf.Abs(accData[1]) + Mathf.Abs(accData[2]);
    if (forceMagnitudeApprox > 8192 && forceMagnitudeApprox < 32768) {
	
		// Turning around the X axis results in a vector on the Y-axis
        pitchAcc = Mathf.Atan2(accData[1], accData[2]) * 180 / M_PI;
        pitch = pitch * 0.98 + pitchAcc * 0.02;
 
		// Turning around the Y axis results in a vector on the X-axis
        rollAcc = Mathf.Atan2(accData[0], accData[2]) * 180 / M_PI;
        roll = roll * 0.98 + rollAcc * 0.02;
    }
} 
*/

