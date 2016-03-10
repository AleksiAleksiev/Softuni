define(['event', 'employee','extensions'], function (Event, Employee) {
    return (function () {
        function Party(options) {
            Event.call(this, options);
            this.setIsCatered(options.isCatered);
            this.setIsBirthday(options.isBirthday);
            this.setOrganiser(options.organiser);
        }

        Party.extends(Event);
        Party.prototype.getIsCatered = function () {
            return this._isCatered;
        };
        Party.prototype.setIsCatered = function (isCatered) {
            this._isCatered = Boolean(isCatered);
        };
        Party.prototype.getIsBirthday = function () {
            return this._isBirthday;

        };
        Party.prototype.setIsBirthday = function (isBirthday) {
            this._isBirthday = Boolean(isBirthday);
        };
        Party.prototype.getOrganiser = function () {
            return this._organiser;
        };
        Party.prototype.setOrganiser = function (organiser) {
            if (!(organiser instanceof Employee)) {
                throw new Error('Invalid organiser argument');
            }
            this._organiser = organiser;
        };
        return Party;
    })();
});