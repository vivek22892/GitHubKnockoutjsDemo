
$(document).ready(function () {
    $("#Scs").hide();
    $("#Dgrmsg").hide();
    loaddata();
});

function loaddata() {
    var EmpViewModel = function () {

        //Make the self as 'this' reference
        var self = this;

        //Declare observable which will be bind with UI
        self.EmpNo = ko.observable();
        self.EmpName = ko.observable();
        self.Salary = ko.observable();
        self.DeptName = ko.observable();
        self.Designation = ko.observable();

        //The Object which stored data entered in the observables
        var EmpData = {
            EmpNo: self.EmpNo,
            EmpName: self.EmpName,
            Salary: self.Salary,
            DeptName: self.DeptName,
            Designation: self.Designation
        };

        //Declare an ObservableArray for Storing the JSON Response
        self.Employees = ko.observableArray([]);

        GetEmployees(); //Call the Function which gets all records using ajax call

        //Function to perform POST (insert Employee) operation
        self.save = function () {
            //Ajax call to Insert the Employee
            $.ajax({
                type: "POST",
                url: "/Employee/UpdateEmployee",
                data: ko.toJSON(EmpData), //Convert the Observable Data into JSON
                contentType: "application/json",
                success: function (data) {
                    if (data.EmpNo == 0 || data.EmpNo == null) {
                        var msg = "Please fill all Required fields";
                        ErrorMessage(msg);
                    } else {
                        self.EmpNo(data.EmpNo);
                        var msg = "The New Employee Id :" + self.EmpNo() + " " + "is Added Successfully";
                        AlertMessage(msg)
                        ClearDataTable();
                        ClearBindings(self);
                        GetEmployees();
                    }
                },
                error: function () {
                    alert("Failed");
                }
            });
        };

        self.update = function () {
            $.ajax({
                type: "PUT",
                url: "/Employee/EditEmployee",
                data: ko.toJSON(EmpData),
                contentType: "application/json",
                success: function (data) {
                    if (data.EmpNo == 0 || data.EmpNo == null) {
                        var msg = "Please fill  all Required fields";
                        ErrorMessage(msg);
                    } else {
                        var msg = "Record Updated Successfully";
                        AlertMessage(msg)
                        ClearDataTable();
                        ClearBindings(self);
                        GetEmployees();
                    }
                },
                error: function (error) {
                    alert(error.status + "<!----!>" + error.statusText);
                }
            });
        };

        //Function to perform DELETE Operation
        self.deleterec = function (employee) {
            $.ajax({
                type: "DELETE",
                url: "/Employee/DeleteEmployee" + "/" + employee.EmpNo,
                success: function (data) {
                    var msg = "Record Deleted Successfully";
                    AlertMessage(msg)
                    ClearDataTable();
                    GetEmployees();//Refresh the Table
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        };

        //Function to Read All Employees
        function GetEmployees() {
            //Ajax Call Get All Employee Records
            $.ajax({
                type: "GET",
                url: "/Employee/GetDetails",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    self.Employees([]);
                    for (var i = 0; i < data.length; i++) {
                        self.Employees.push(data[i]);
                    }
                    $('#dtBasicExample').DataTable();
                    $('.dataTables_length').addClass('bs-select');
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }

        //Function to Display record to be updated. This will be
        //executed when record is selected from the table
        self.getselectedemployee = function (employee) {
            self.EmpNo(employee.EmpNo),
            self.EmpName(employee.EmpName),
            self.Salary(employee.Salary),
            self.DeptName(employee.DeptName),
            self.Designation(employee.Designation)
        };
    };

    ko.applyBindings(new EmpViewModel());
}
function ClearBindings(self) {
    self.EmpNo(null);
    self.EmpName(null);
    self.Salary(null);
    self.Designation(null);
    self.DeptName(null);
    self.Employees([]);
}

function ClearDataTable() {
    var oTable = $('#dtBasicExample').dataTable();
    if (oTable != null) {
        oTable.fnClearTable();
        oTable.fnDestroy();
    }
}

function AlertMessage(msg) {
    $("#Scs").empty();
    $("#Scs").append(msg);
    $("#Scs").show();
    $('#Scs').delay(3000).fadeOut();
}
function ErrorMessage(msg) {
    $("#Dgrmsg").empty();
    $("#Dgrmsg").append(msg);
    $("#Dgrmsg").show();
    $('#Dgrmsg').delay(3000).fadeOut();
}