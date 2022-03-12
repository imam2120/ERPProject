 
$(document).ready(function () {
    console.log(window.location.href);
    console.log(window.location.origin);

   /* alert('test 1');*/
    $("#grid").kendoGrid().empty();
    debugger;
    var result = CallAjax_POST('GetCompanyInfo/ProductInfo', '', false, false)
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
        $("#grid").kendoGrid().empty();
        debugger;      
        $("#grid").kendoGrid({
            dataSource: result,
            resizable: true,
 /*           height: 550,*/
            pageable: true,
            pageable: {
                input: false,
                numeric: true,
                butonCount: 5,
                pageSize: 20,
                alwaysVisible: true,
                previousNext: true,
                serverPaging: true,
                serverFiltering: true,
            },
            scrollable: true,
           /* filterable: true,*/
            //pageSize: 20,
            //height: 550,
          // toolbar: ["create"],
            toolbar: "<a id='btnSave' role='button' class='btn btn-info' href='javascript:void(0)'>Create Product<i class='fa fa-file-excel-o' aria-hidden='true'></i> </a>",
            selectable: true,
            editable: false,
            sortable: true,
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
                { field: "Edit ", title: "Action", width: "150px", template: "<Center><button type='button' class='btn btn-success btn-sm' id='btnEdit'>Edit</button>&nbsp;&nbsp;<button type='button' class='btn btn-danger btn-sm' id='btnDelete'>Delete</button></Center>"}
            ],
         
        });   

    }
   
   
   
});


$(document).on("click", "#btnSave", function () {

    $('#modal-lg').modal('toggle');
});

$(document).on("click", "#btnEdit", function () {
  
    var grid = $("#grid").data("kendoGrid");
    var dataItem = grid.dataItem($(this).closest('tr'));
    $("#hdnCompanyId").val(dataItem.CompanyId);
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











