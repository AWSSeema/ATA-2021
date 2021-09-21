var Common = {

    ConvertDate: function (date) {
        var acutaldate = eval(date.replace(/\/Date\((.*?)\)\//gi, "new Date($1)"));
        var mmddyyyydate = acutaldate.getMonth() + 1 + "/" + acutaldate.getDate() + "/" + acutaldate.getFullYear();
        return mmddyyyydate;
    },

   IsNumeric:function (strString)
   {
    var strValidChars = "0123456789.-,";
    var strChar;
    var blnResult = true;

    if (strString.length == 0) return false;

    //  test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult == true; i++)
    {
    strChar = strString.charAt(i);
    if (strValidChars.indexOf(strChar) == -1)
    {
    blnResult = false;
    }
    }
    return blnResult;
    },

    isExcel: function (url) {
        var extention = Common.getFileExtension(url);
        // available image extensions
        var available_ext = new Array('xlsx', 'xls');
        for (var i = 0; i < available_ext.length; i++) {
            if (extention == available_ext[i])
                return true;
        }
        return false;
    },

    isImage: function (url) {
        var extention = Common.getFileExtension(url);
        // available image extensions
        var available_ext = new Array('jpg', 'png', 'PNG', 'gif', 'jpeg', 'JPEG');
        for (var i = 0; i < available_ext.length; i++) {
            if (extention == available_ext[i])
                return true;
        }
        return false;
    },

    isDocument: function (url) {
        var extention = Common.getFileExtension(url);
        // available image extensions
        var available_ext = new Array('pdf', 'txt', 'doc', 'docx');
        for (var i = 0; i < available_ext.length; i++) {
            if (extention == available_ext[i])
                return true;
        }
        return false;
    },

    getFileExtension: function (filename) {
        var ext = /^.+\.([^.]+)$/.exec(filename);
        return ext == null ? "" : ext[1].toLowerCase();
    },

    ValidatePassword: function (value) {
        var aChar = /[A-Za-z]/;
        var aNumber = /[0-9]/;
        var Exsym = /[@#$%!^&*?]/
        if (value.length < 8 || value.length > 16 || value.search(aChar) == -1 || value.search(aNumber) == -1 || value.search(Exsym) == -1) {
            return false;
        }
        else {
            return true;
        }
    },

    Closebtn:function() {
        $('.closable').append('<span class="closelink" title="Close"></span>');
        $('.closelink').click(function () {
            $(this).parent().fadeOut('600', function () { $(this).remove(); });
        });
    },

    BtnDisable:function()
    {
        $('input[type=submit]').click(function (evt) {
            evt.preventDefault();
            var self = $(this);
            var frm = self.closest('form');
            frm.validate();
            if (frm.valid()) {
                frm.submit();
                self.attr('disabled', 'disabled');
                self.hide();
                //self.attr('value', 'Please wait....');
            }
        });
    },

    Disable: function () {

        //$("#divload").show();

    },

    Pageno: function (page) {

        var pageno = '';
        switch (page) {
            case "«":
                pageno = parseInt($("#hdnPageNo").val()) - 1;
                break;
            case "»":
                pageno = parseInt($("#hdnPageNo").val()) + 1;
                break;
            default:
                pageno = page;
                break;
        }
        $('#hdnPageNo').val(pageno);
        return pageno;
    },

    getCheckedRadio: function (Radio) {
        var rvalue = 0;
        var radioButtons = document.getElementsByName(Radio);
        for (var x = 0; x < radioButtons.length; x++) {
            if (radioButtons[x].checked) {
                rvalue = radioButtons[x].value;
            }
        }
        return rvalue;
    },

    CheckEmail: function (address) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(address)) {
            return (true)
        }
        return (false)
    },
    UploadDocument: function () {

        if ($('#ufile').val() != '') {

            $.ajaxFileUpload({
                url: 'Handlers/FileUpload.ashx?key=insert&subFolder=News',
                secureuri: false,
                fileElementId: 'ufile',
                dataType: 'json',
                success: function (data, status) {
                    $('#divDocmessage').html('<div class="success">Uploaded Document Successfully.</div>');
                    $('#ufile').val('');
                },
                error: function (data, status, e) {
                    $('#divDocmessage').html('<div class="success">Uploaded Document Successfully.</div>');
                    $('#ufile').val('');
                }
            }
	            )
        }
    },

    ValidateUpload: function () {

        if ($('#ctl00_CPH1_txtPhotoTitle').val() == "") { alert('Please Enter Title'); $('#ctl00_CPH1_txtPhotoTitle').focus(); return false; }
        if ($('#ctl00_CPH1_fuPhotoUpload').val() == "") { alert('Please Select File'); $('#ctl00_CPH1_fuPhotoUpload').focus(); return false; }
        return true;

    },

    AddPhoto: function (PageId) {
        $('#divBlog').hide();
        $('#divInteviews').hide();
        $('#divsearch').hide();
        $('#ctl00_CPH1_hdnPhotoId').val(PageId);
        $('#divPhoto').show();


    },
    selectAll: function (check, check1) {
        //var aa = document.getElementById('from2');
        var aa = document.getElementsByName(check);
        var aa1 = document.getElementsByName(check1);
        aa1[0].checked = true;
        for (var i = 0; i < aa.length; i++) {
            aa[i].checked = true;
        }
    },
    clearAll: function (check, check1) {
        var aa = document.getElementsByName(check);
        var aa1 = document.getElementsByName(check1);
        aa1[0].checked = false;
        for (var i = 0; i < aa.length; i++) {
            aa[i].checked = false;
        }
    },

    Close: function () {
        $('.closable').append('<span id="lblclose" class="closelink" title="Close"></span>');
        $('.closelink').click(function () {
            $(this).parent().fadeOut('600', function () { $(this).remove(); });
        });
    },

    isNumberKey: function (evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    },

    GetCheckBoxValue: function (CbName) {
        var returnval = "";
        for (var i = 0; i < $("input[name='" + CbName + "']").length; i++) {
            if ($("input[id='" + CbName + "-" + (i + 1) + "']").is(":checked")) {
                returnval = returnval + $("input[id='" + CbName + "-" + (i + 1) + "']").val() + ",";
            }
        }
        var strLen = returnval.length;
        returnval = returnval.slice(0, strLen - 1);
        return returnval;
    },

    opendialog: function (heading, div) {
        $('#' + div).dialog('option', 'title', heading);
        $("#" + div).dialog("open");
    },

    closedialog: function (div) {
        $("#" + div).dialog("close");
    },

    EncodeURL: function (input) {
        return input.replace("-", ".").replace("/", "_").replace(" ", "-");
    },

    DecodeURL: function (input) {
        return input.replace("_", "/").replace("-", " ").replace(".", "-");
    },

    cartvalidate:function(i)
    {
        if ($("#txtamount-" + i).val() != '') {
            if (!Common.IsNumeric($("#txtamount-" + i).val())) { alert('Please enter valid price.'); $("#txtamount-" + i).focus(); return false; }
        }
        else { alert('Please enter donation amount.'); $("#txtamount-" + i).focus(); return false; }
    return true;
},
    
    CheckEmailAvailability: function (email) {
var returnval = null;
$.ajax({
    type: "POST",
    url: '@Url.Action("CheckEmailAvailability", "Account")', // the URL of the controller action method
    data: { Email: email }, // optional data
    datatype: "JSON",
    async: false,
    success: function (result) {
        if (result.ok) {
            returnval = result.data;
        }
    }
});
return returnval;
    },
    CheckUserNameAvailability:function(username) {
var returnval = null;
$.ajax({
    type: "POST",
    url: '@Url.Action("CheckUserNameAvailability", "Account")', // the URL of the controller action method
    data: { UserName: username }, // optional data
    datatype: "JSON",
    async: false,
    success: function (result) {
        if (result.ok) {
            returnval = result.data;
        }
    }
});
return returnval;
},
    //Charecter Count
    upper:function(ustr) {
var str=ustr.value;
ustr.value=str.toUpperCase();
},
    lower:function(ustr) {
    var str = ustr.value;
    ustr.value = str.toLowerCase();

},

    CapitalFirstLetter:function(word) {
    word.value = word.value.substr(0, 1).toUpperCase() + word.value.substr(1);
},

    CapitalFirstLetterInWord:function(obj) {
    val = obj.value;
    newVal = '';
    val = val.split(' ');
    for (var c = 0; c < val.length; c++) {
        newVal += val[c].substring(0, 1).toUpperCase() +
     val[c].substring(1, val[c].length) + ' ';
    }
    obj.value = newVal;
},

       
    validate:function(key) {
    //getting key code of pressed key
    var keycode = (key.which) ? key.which : key.keyCode;
    //comparing pressed keycodes
    if ((keycode >= 65 && keycode <= 90) || (keycode >= 97 && keycode <= 122) || (keycode == 8 || keycode == 32 || keycode == 46)) {
        return true;
    }
    else {
        return false;
    }
},

    TxtCounters: function (id, max_length, myelement) {
        counter = document.getElementById(id);
        field = document.getElementById(myelement).value;
        field_length = field.length;
        if (field_length <= max_length) {
            //Calculate remaining characters
            remaining_characters = max_length - field_length;
            //Update the counter on the page
            counter.innerHTML = remaining_characters;
        }
    },

    AddToWishList: function (UserId) {
        if (UserId == 0) {
            alert("Please login to continue.");
            showlogin();
            return false;
        }
        else {
            return true;
        }
    },

    addvaluetodb: function (ProductId, ProductName, Items,Quantity, CartType, i,cartcnt) {
        
        if (cartcnt >= 5 && CartType == 'sample') {
            alert('Sample Cart Limit to be Maximum of 5 Cards Only');
        }
        else{
            if (Items > 0) {
                var objImageCart = {};
                objImageCart.CartDetailId = 0;
                objImageCart.ProductId = ProductId;
                objImageCart.Quantity = Quantity;
                objImageCart.CartType = 'sample';

                $.ajax({
                    url: 'Products/ProductCartInsert',
                    type: 'POST',
                    data: JSON.stringify(objImageCart),
                    datatype: "JSON",
                    contentType: 'application/json',
                    success: function (result) {
                        var div = '';
                        if (result.ok) {
                            var ccc = $('.CCount')[0].innerHTML;
                            $('.CCount').html(parseInt(ccc) + parseInt(1));
                            $('#cartitems').html(result.data + " Item(s)");
                            if (CartType == "sample") {
                                $('#bbcart-' + i).addClass('hide');
                                $('#dbcart-' + i).removeClass('hide');
                                $('#bscart-' + i).addClass('hide');
                                $('#ascart-' + i).removeClass('hide');
                            }
                            else {
                                $('#bscart-' + i).addClass('hide');
                                $('#dscart-' + i).removeClass('hide');
                                $('#bbcart-' + i).addClass('hide');
                                $('#abcart-' + i).removeClass('hide');
                            }
                            $("#divmessage").html("<div class=\"success closable\">Item Added to Your Cart</div>");
                            $('.closable').append('<span class="closelink" title="Close"></span>');
                            $('.closelink').click(function () {
                                $(this).parent().fadeOut('600', function () { $(this).remove(); });
                            });
                        }
                        else {
                            if (result.data == 2) {
                                alert("CANNOT ADD BULK ORDER WITH SAMPLE CARD(S)");
                            }
                            if (result.data == 3) {
                                alert("CANNOT ADD SAMPLE CARD WITH BULK ORDER");
                            }
                            //$("#divmessage").html(result.data);
                        }
                    }
                });
            }
            else {
                alert('No stock for selected item.we will update soon');
            }
        }
       
    },

    addordertodb: function (ProductId, ProductName, Items, Quantity, CartType, i, cartcnt) {

        if (cartcnt >= 5 && CartType == 'sample') {
            alert('Sample Cart Limit to be Maximum of 5 Cards Only');
        }
        else {
            if (Items > 0) {
                var objImageCart = {};
                objImageCart.CartDetailId = 0;
                objImageCart.ProductId = ProductId;
                objImageCart.Quantity = Quantity;
                objImageCart.CartType = 'sample';

                $.ajax({
                    url: 'Products/ProductCartInsert',
                    type: 'POST',
                    data: JSON.stringify(objImageCart),
                    datatype: "JSON",
                    contentType: 'application/json',
                    success: function (result) {
                        var div = '';
                        if (result.ok) {
                            //var ccc = $('.CCount')[0].innerHTML;
                            //$('.CCount').html(parseInt(ccc) + parseInt(1));
                            //$('#cartitems').html(result.data + " Item(s)");
                            window.location.href = 'cart-details.html';
                            $("#divmessage").html("<div class=\"success closable\">Item Added to Your Cart</div>");
                            $('.closable').append('<span class="closelink" title="Close"></span>');
                            $('.closelink').click(function () {
                                $(this).parent().fadeOut('600', function () { $(this).remove(); });
                            });
                        }
                        else {
                            if (result.data == 2) {
                                alert("CANNOT ADD BULK ORDER WITH SAMPLE CARD(S)");
                            }
                            if (result.data == 3) {
                                alert("CANNOT ADD SAMPLE CARD WITH BULK ORDER");
                            }
                            //$("#divmessage").html(result.data);
                        }
                    }
                });
            }
            else {
                alert('No stock for selected item.we will update soon');
            }
        }

    },

    mfadeout: function () {
        setTimeout(function () { $('.success').fadeOut(6000); }, 6000);
        setTimeout(function () { $('.error').fadeOut(6000); }, 6000);
    },

    GetCaptcha: function () {
        var returnval = null;
        $.ajax({
            url: 'Account/GetCaptcha',
            type: 'POST',
            datatype: "JSON",
            async: false,
            success: function (result) {
                if (result.ok) {
                    returnval = result.data;
                }
                //else {
                //    alert(result.data);
                //}
            }
        });
        return returnval;
    }

}
