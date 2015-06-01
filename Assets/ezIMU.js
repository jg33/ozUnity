#pragma strict

private var ACCELEROMETER_SENSITIVITY:float =  8192.0;
private var GYROSCOPE_SENSITIVITY:float = 65.536;
 
private var M_PI:float = 3.14159265359;
 
private var dt:float = 0.01;				// 10 ms sample rate!    
 

public var pitch:float;
public var roll:float;

function Start () {

}

function Update () {
	var _gyrData:float[] = [0f,0f,0f];
	var _accData:float[] = [0f,0f,0f];

	if (Input.isGyroAvailable){
		var rawGyro:Vector3 = Input.gyro.rotationRateUnbiased;

		_gyrData[0] = rawGyro.x;
		_gyrData[1] = rawGyro.y;
		_gyrData[2] = -rawGyro.z;
		
		//transform.localRotation.x = gyrData[0];
		//transform.localRotation.y = gyrData[1];
		//transform.localRotation.z = gyrData[2];
	}
	var rawAcc:Vector3 = Input.acceleration;
	_accData[0] = rawAcc.x;
	_accData[1] = rawAcc.y;
	_accData[2] = rawAcc.z;

	
	ComplementaryFilter(_accData,_gyrData);
	
	transform.localRotation.x = pitch;
	transform.localRotation.y = roll;
	transform.localRotation.z = _gyrData[2];
}


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

