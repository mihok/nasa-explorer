var MiniMap;

//Updates and draws the minimap
//TODO: Implement a map loop
MiniMap = (function () {
    //Constructor
    function MiniMap() {
        this.map = $("#map")[0].getContext("2d");
        this.options = {};
        this.options.point_radius = 1;
        this.options.point_color = "#FFF";
        this.options.width = 200;
        this.options.height = 200;
        this.points = [];

        this.normalizePoints();
    }

    //Starts the map loop
    MiniMap.prototype.start = function (points) {
        console.log("Starting");
        this.points.push(points);
        this.draw();
    }

    //Draws the minimap
    MiniMap.prototype.draw = function () {
        this.map.fillStyle = "#222";
        this.map.fillRect(0, 0, this.options.width, this.options.height);

        this.map.beginPath();
        this.map.fillStyle = this.options.point_color;
        for (i in this.points) {
            console.log("Point: x=" + (this.points[i].x + 100) + ", y=" + (this.points[i].y + 100));
            this.map.arc(this.points[i].x + 100, this.points[i].y + 100, this.options.point_radius, 0, 2 * Math.PI, false);
            this.map.fill();
        }
    }

    //Add point
    MiniMap.prototype.addPoint = function (x, y) {
        this.points.push({ "x": x, "y": y });
        this.draw();
    }

    //Brings point coordinates in range (0..map.size) to draw them properly
    MiniMap.prototype.normalizePoints = function () {
        /*
        var max = {x: 0, y: 0}
        var min = {x: Number.POSITIVE_INFINITY, y: Number.POSITIVE_INFINITY}
        for(i in points) {
        max.x = Math.max(max.x, points[i].x);
        max.y = Math.max(max.y, points[i].y);
        min.x = Math.min(min.x, points[i].x);
        min.y = Math.min(min.y, points[i].y);
        }

        for(i in points) {
        points[i].x = (((points[i].x-min.x)/(max.x-min.x))*190)+5;
        points[i].y = (((points[i].y-min.y)/(max.y-min.y))*190)+5;
        }
        */
    }

    return MiniMap;
})();