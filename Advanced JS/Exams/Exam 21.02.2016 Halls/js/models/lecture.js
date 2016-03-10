define(['event', 'trainer', 'course', 'extensions'], function (Event, Trainer, Course) {
    return (function () {
        function Lecture(options) {
            Event.call(this, options);
            this.setTrainer(options.trainer);
            this.setCourse(options.course);
        }

        Lecture.extends(Event);
        Lecture.prototype.getTrainer = function () {
            return this._trainer;
        };
        Lecture.prototype.setTrainer = function (trainer) {
            if (!(trainer instanceof Trainer)) {
                throw new Error('Invalid trainer argument')
            }
            this._trainer = trainer;
        };
        Lecture.prototype.getCourse = function () {
            return this._course;

        };
        Lecture.prototype.setCourse = function (course) {
            if (!(course instanceof Course)) {
                throw new Error('Invalid course argument.')
            }
            this._course = course;
        };
        return Lecture;
    })();
});