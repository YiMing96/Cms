
if (top.miniuijs) {

    eval(top.miniuijs);
} else {
//    document.write('<script src="' + bootPATH + 'miniui/miniui.js" type="text/javascript" ></sc' + 'ript>');
    
    $.ajax({
        url: bootPATH + 'miniui/miniui.js',
        async: false,
        dataType: 'text',
        success: function (text) {
            window.miniuijs = text;
            //eval("("+text+")")            
        }
    });

    eval(window.miniuijs);    
}


