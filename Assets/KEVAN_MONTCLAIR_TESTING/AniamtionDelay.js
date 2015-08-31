var startDelay : int;
  
  function Start()
  {
  var animator = GetComponent(Animator);
  yield WaitForSeconds(startDelay);
  animator.Play("Take_001");
  }