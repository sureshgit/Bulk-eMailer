//Code for creating 
function fillCell(row, cellNumber, text) {
    var cell = row.insertCell(cellNumber);
    cell.innerHTML = text; cell.style.borderBottom = cell.style.borderRight = "solid 1px #aaaaff"; 
  }


function addToClientTable(name, text)
 {
     var table = document.getElementById("<%= clientSide.ClientID %>");
     var row = table.insertRow(0); fillCell(row, 0, name);
     fillCell(row, 1, text);
 }

function uploadError(sender, args) 
{
    addToClientTable(args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>"); 
}


function uploadComplete(sender, args)
 {
    var contentType = args.get_contentType();
    var text = args.get_length() + " bytes";
    if (contentType.length > 0) 
    {
        text += ", '" + contentType + "'";
    }
    addToClientTable(args.get_fileName(), text);
} 
 
 
 function uploadStart(sender, args) {
     var filename = args.get_fileName(); 
     var filext = filename.substring(filename.lastIndexOf(".") + 1);    
     if (filext == "jpg" || filext == "gif" || filext == "png") {
         return true;            
     }
     else
      {  //you cannot cancel the upload using set_cancel(true)
      //cause an error      
      //will  automatically trigger event OnClientUploadError    
      var err = new Error();
      err.name = 'My API Input Error';
      err.message = 'Only .jpg, .gif, .png files';
      throw (err);
      return false;
  }
  } 