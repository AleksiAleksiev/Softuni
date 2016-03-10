define(['party','lecture'], function (Party, Lecture) {
    return (function () {
        function Hall(name, capacity) {
            this.setName(name);
            this.setCapacity(capacity);
            this.parties = [];
            this.lectures = [];
        }
        Hall.prototype.getName = function () {
            return this._name;
        };
        Hall.prototype.setName = function (name) {
            if (typeof name !== 'string' || /[^A-Za-z\s]/.test(name)) {
                throw new Error('Invalid format of Name.')
            }
            this._name = name;
        };
        Hall.prototype.getCapacity = function () {
            return this._capacity;
        };
        Hall.prototype.setCapacity = function (capacity) {
            if (typeof capacity !== 'number' || /[^\d]/.test(capacity.toString())) {
                throw new Error('Invalid format of Capacity.')
            }
            this._capacity = capacity;
        };
        Hall.prototype.addEvent = function (event) {
            if (event instanceof Party) {
                this.parties.push(event);
            } else if (event instanceof Lecture) {
                this.lectures.push(event);
            } else {
                throw new Error('Object must be a party or lecture');
            }
        };
        return Hall;
    })();
});