define([], function () {
    return (function () {
        function Course(name, numberOfLectures) {
            this.setName(name);
            this.setNumberOfLectures(numberOfLectures);
        }

        Course.prototype.getName = function () {
            return this._name;
        };
        Course.prototype.setName = function (name) {
            if (typeof name !== 'string' || /[^A-Za-z\s]/.test(name)) {
                throw new Error('Invalid format of Name.')
            }
            this._name = name;
        };
        Course.prototype.getNumberOfLectures = function () {
            return this._numberOfLectures;
        };
        Course.prototype.setNumberOfLectures = function (numberOfLectures) {
            if (typeof numberOfLectures !== 'number' || /[^\d]/.test(numberOfLectures.toString())) {
                throw new Error('Invalid format of Number of Lectures.')
            }
            this._numberOfLectures = numberOfLectures;
        };

        return Course;
    })();
});