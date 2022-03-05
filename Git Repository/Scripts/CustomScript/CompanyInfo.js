﻿ 
$(document).ready(function () {

   /* alert('test 1');*/
    $("#grid").kendoGrid().empty();
    debugger;
    var result = CallAjax_POST('GetCompanyInfo/Index', '', false, false)
    if (result.length > 0) {
                //    $("#dvExport").css('display', 'block');
                //else
                //    $("#dvExport").css('display', 'none');
                //alert("Test");
            PopulateGridView(result);
}else {
        alert("Error! Please Contact..");
    //        $("#dvExport").css('display', 'none');
}
   
    function PopulateGridView(result) {
        debugger;      
        $("#grid").kendoGrid({
            dataSource: result,
            pageable: true,
           /* filterable: true,*/
            pageSize: 20,
            //height: 550,
            // toolbar: ["create"],
          selectable: true,
            search: {
                fields: [
                    { name: "CompanyId", operator: "eq" },
                    { name: "CompanyName", operator: "contains" },
                    { name: "address", operator: "contains" },

                ]
            },
            columns: [

                { field: "CompanyId", title: "Company Id", filterable: true, filterable: { multi: true, search: true }, title: "Company Id", width: "120px" },
                { field: "CompanyName", title: "Company Name", filterable: true, filterable: { multi: true, search: true }, title: "Company Name", width: "120px" },
                { field: "Address", filterable: true, filterable: { multi: true, search: true }, width: "120px" },
                { field: "Contact", title: "Contact Number",filterable: { multi: true, search: true }, title: "Company Name", width: "120px" },
                 { field: "Email", title: "Email", filterable: { multi: true, search: true }, width: "120px" },
                { field: "WebAddress", title: "Web Address", filterable: { multi: true, search: true }, width: "120px" },
                { field: "Edit ", title: "Action", width: "150px", template: "<button type='button' class='btn btn-success btn-sm' id='btnEdit'>Edit</button>&nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' id='btnDelete'>Delete</button>"}
            ],
         
        });


        

    }
   
   
   
});

$(document).on("click", "#btnEdit", function () {
  
    var grid = $("#grid").data("kendoGrid");
    var dataItem = grid.dataItem($(this).closest('tr'));
    $("#txtCompanyName").val(dataItem.CompanyName);
    $("#txtAddress").val(dataItem.Address);
    $("#txtContactNo").val(dataItem.Contact);
    //alert(dataItem.CompanyName);
   // alert(dob);
});


$(document).on("click", "#btnDelete", function () {

    var grid = $("#grid").data("kendoGrid");
    var dataItem = grid.dataItem($(this).closest('tr'));
    //$("#txtCompanyName").val(dataItem.CompanyName);

    var result = CallAjax_POST('GetCompanyInfo/Index', '', false, false)

    //alert(dataItem.CompanyName);
    // alert(dob);

   
});











