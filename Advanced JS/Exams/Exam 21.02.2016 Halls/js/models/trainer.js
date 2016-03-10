define(['employee', 'course', 'extensions'], function (Employee, Course) {
    return (function (eventsSystem) {
        function Trainer(name, workHours) {
            Employee.call(this, name, workHours);
            this.courses = [];
            this.feedbacks = [];
        }
        Trainer.extends(Employee);
        Trainer.prototype.addFeedback = function (feedback) {
            if (typeof feedback !== 'string') {
                throw new Error('Feedback must be a string');
            }
            this.feedbacks.push(feedback);
        };
        Trainer.prototype.addCourse = function (course) {
            if (!(course instanceof Course)) {
                throw new Error('Object being added must be of type Course');
            }
            this.courses.push(course);
        };
        return Trainer;
    })();
});