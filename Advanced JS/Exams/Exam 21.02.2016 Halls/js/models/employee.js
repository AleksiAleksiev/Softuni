define([], function () {
    return (function () {
        function Employee(name, workHours) {
            this.setName(name);
            this.setWorkhours(workHours);
        }

        Employee.prototype.getName = function () {
            return this._name;
        };
        Employee.prototype.setName = function (name) {
            if (typeof name !== 'string' || /[^A-Za-z\s]/.test(name)) {
                throw new Error('Invalid format of Name.')
            }
            this._name = name;
        };
        Employee.prototype.getWorkhours = function () {
            return this._workHours;
        };
        Employee.prototype.setWorkhours = function (workHours) {
            if (typeof workHours !== 'number' || /[^\d]/.test(workHours.toString())) {
                throw new Error('Invalid format of Workhours.')
            }
            this._workHours = workHours;
        };
        return Employee;
    })();
});