define([], function () {
    return (function () {

        function Event(options) {
            if (this.constructor === Event) {
                throw new Error('Abstract classes cannot be instantiated.');
            }
            this.setTitle(options.title);
            this.setType(options.type);
            this.setDuration(options.duration);
            this.setDate(options.date);
        }

        Event.prototype.getTitle = function () {
            return this._title;
        };
        Event.prototype.setTitle = function (title) {
            if (typeof title !== 'string' || /[^A-Za-z\s]/.test(title)) {
                throw new Error('Invalid format of Title.')
            }
            this._title = title;
        };
        Event.prototype.getType = function () {
            return this._type;
        };
        Event.prototype.setType = function (type) {
            if (typeof type !== 'string' || /[^A-Za-z\s]/.test(type)) {
                throw new Error('Invalid format of Type.')
            }
            this._type = type;
        };
        Event.prototype.getDuration = function () {
            return this._duration;
        };
        Event.prototype.setDuration = function (duration) {
            if (typeof duration !== 'number' || /[^\d]/.test(duration.toString())) {
                throw new Error('Invalid format of Duration.')
            }
            this._duration = duration;
        };
        Event.prototype.getDate = function () {
            return this._date;
        };
        Event.prototype.setDate = function (date) {
            if (!(date instanceof Date)) {
                throw new Error('Invalid format of Date.')
            }
            this._date = date;
        };
        return Event;
    })();
});