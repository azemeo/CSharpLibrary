function Trim( strings ) {
	var reg1 = /^\s*/; 
	var reg2 = /\s*$/; 
	var result = strings.replace(reg1, "");
	result = result.replace(reg2, "");
	return result;
}

function txtIsNullAlertDiv(input)
{
	bl = true;
	if(Trim(document.all(input).value) == "")
	{	
		window.showModalDialog('RegLost.aspx',window,' dialogHeight: 335px; dialogWidth: 404px; edge: Raised; center: Yes; help: No; resizable: No; scroll: No; status: No;');
		document.all(input).focus();
		bl = false;
	}
	return bl;
}

function txtIsNull(input,info)
{
	bl = true;
	if(Trim(document.all(input).value) == "")
	{	
		alert(info);
		document.all(input).focus();
		//document.all(input).select();
		bl = false;
	}
	return bl;
}

function check(inputtype, inputname, other, mes, foucsinput, foucsmes)
{	
	var flag = false;
	var flagmes = false;
	var r = document.all(inputname);
	for(var i=0; i< document.forms[0].elements.length; i++)
    {		
        var e = document.forms[0].elements[i];
        if( (e.type==inputtype) && e.name.indexOf(inputname) >=0 && (e.checked == true) )
        {
        /*
			if ( r[r.length - 1 ].checked == true && other == true && Trim(document.all(foucsinput).value) == "" )
			{
				flagmes = true;
				alert(foucsmes);
				document.all(foucsinput).focus();
				break;
            }
       */
            //else
            //{
				flag = true;				
				break;
            //}           
        }
     }
     
     if ( r[r.length - 1 ].checked == true && other == true && Trim(document.all(foucsinput).value) == "" )
	 {
		flag = false;
		flagmes = true;
		alert(foucsmes);
		document.all(foucsinput).focus();
		//document.all(foucsinput).select();
     }
	   
     if ( r[r.length - 1 ].checked != true && other == true && Trim(document.all(foucsinput).value) != "" )
	 {
		document.all(foucsinput).value = "";    
	 }
	 
     if( flag == false && flagmes == false )
     {
		if( r != null )
		{
			if( r.length != null )
			{
				for( var i = 0;i < r.length; i ++ )
				{
					try
					{
						if( r[i].disabled == false )
						{
							r[i].focus();
							break;
						}
					}
					catch(e) {}
				}
			}
		}
		//try {
		//r[0].focus(); //} catch(e) {r[2].focus();}
		alert(mes);
     }
     return flag;     
}

function check1(inputtype, inputname, other, mes, foucsinput, foucsmes)
{	
	var flag = false;
	var flagmes = false;
	var r = document.all(inputname);
     if( document.forms[1] != null)
     {
		for(var i=0; i< document.forms[1].elements.length; i++)
		{		
			var e = document.forms[1].elements[i];
			if( (e.type==inputtype) && e.name.indexOf(inputname) >=0 && (e.checked == true) )
			{
			/*
				if ( r[r.length - 1 ].checked == true && other == true && Trim(document.all(foucsinput).value) == "" )
				{
					flagmes = true;
					alert(foucsmes);
					document.all(foucsinput).focus();
					break;
				}
		*/
				//else
				//{
					flag = true;				
					break;
				//}           
			}
		}
     }
     
     if ( r[r.length - 1 ].checked == true && other == true && Trim(document.all(foucsinput).value) == "" )
	 {
		flag = false;
		flagmes = true;
		alert(foucsmes);
		document.all(foucsinput).focus();
		//document.all(foucsinput).select();
     }
	   
     if ( r[r.length - 1 ].checked != true && other == true && Trim(document.all(foucsinput).value) != "" )
	 {
		document.all(foucsinput).value = "";    
	 }
	 
     if( flag == false && flagmes == false )
     {
		if( r != null )
		{
			if( r.length != null )
			{
				for( var i = 0;i < r.length; i ++ )
				{
					try
					{
						if( r[i].disabled == false )
						{
							r[i].focus();
							break;
						}
					}
					catch(e) {}
				}
			}
		}
		//try {
		//r[0].focus(); //} catch(e) {r[2].focus();}
		alert(mes);
     }
     return flag;     
}

function check_null(inputtype, inputname, foucsinput, foucsmes)
{	
	var flag = true;
	var r = document.all(inputname);
     
     if ( r[r.length - 1 ].checked == true && Trim(document.all(foucsinput).value) == "" )
	 {
		flag = false;
		alert(foucsmes);
		document.all(foucsinput).focus();
     }
            
     if ( r[r.length - 1 ].checked != true && Trim(document.all(foucsinput).value) != "" )
	 {
		document.all(foucsinput).value = "";    
	 }

     return flag;     
}