cp:
	number=2 ; while [[ $$number -le 10 ]] ; do \
		echo $$number ;\
		cp -r pc1.app pc$$number.app ; \
		((number = number + 1)) ; \
	done
open: 
	open *.app
close:
	killall pc1




