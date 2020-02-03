! function (e) {
    if ("object" == typeof exports && "undefined" != typeof module) module.exports = e();
    else if ("function" == typeof define && define.amd) define([], e);
    else {
        var t;
        t = "undefined" != typeof window ? window : "undefined" != typeof global ? global : "undefined" != typeof self ? self : this, t.cytoscape = e()
    }
}(function () {
    var define, module, exports;
    return function e(t, r, n) {
        function i(o, s) {
            if (!r[o]) {
                if (!t[o]) {
                    var l = "function" == typeof require && require;
                    if (!s && l) return l(o, !0);
                    if (a) return a(o, !0);
                    var u = new Error("Cannot find module '" + o + "'");
                    throw u.code = "MODULE_NOT_FOUND", u
                }
                var c = r[o] = {
                    exports: {}
                };
                t[o][0].call(c.exports, function (e) {
                    var r = t[o][1][e];
                    return i(r ? r : e)
                }, c, c.exports, e, t, r, n)
            }
            return r[o].exports
        }
        for (var a = "function" == typeof require && require, o = 0; o < n.length; o++) i(n[o]);
        return i
    }({
        1: [function (e, t, r) {
            /*!

            Cytoscape.js 2.7.16 (MIT licensed)

            Copyright (c) The Cytoscape Consortium

            Permission is hereby granted, free of charge, to any person obtaining a copy of
            this software and associated documentation files (the “Software”), to deal in
            the Software without restriction, including without limitation the rights to
            use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
            of the Software, and to permit persons to whom the Software is furnished to do
            so, subject to the following conditions:

            The above copyright notice and this permission notice shall be included in all
            copies or substantial portions of the Software.

            THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
            IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
            FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
            AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
            LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
            OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
            SOFTWARE.

            */
            "use strict"
        }, {}],
        2: [function (e, t, r) {
            "use strict";
            var n = e("./util"),
                i = e("./is"),
                a = e("./promise"),
                o = function (e, t, r) {
                    if (!(this instanceof o)) return new o(e, t, r);
                    var a = this._private = n.extend({
                        duration: 1e3
                    }, t, r);
                    a.target = e, a.style = a.style || a.css, a.started = !1, a.playing = !1, a.hooked = !1, a.applying = !1, a.progress = 0, a.completes = [], a.frames = [], a.complete && i.fn(a.complete) && a.completes.push(a.complete), this.length = 1, this[0] = this
                },
                s = o.prototype;
            n.extend(s, {
                instanceString: function () {
                    return "animation"
                },
                hook: function () {
                    var e = this._private;
                    if (!e.hooked) {
                        var t, r = e.target._private.animation;
                        t = e.queue ? r.queue : r.current, t.push(this), i.elementOrCollection(e.target) && e.target.cy().addToAnimationPool(e.target), e.hooked = !0
                    }
                    return this
                },
                play: function () {
                    var e = this._private;
                    return 1 === e.progress && (e.progress = 0), e.playing = !0, e.started = !1, e.stopped = !1, this.hook(), this
                },
                playing: function () {
                    return this._private.playing
                },
                apply: function () {
                    var e = this._private;
                    return e.applying = !0, e.started = !1, e.stopped = !1, this.hook(), this
                },
                applying: function () {
                    return this._private.applying
                },
                pause: function () {
                    var e = this._private;
                    return e.playing = !1, e.started = !1, this
                },
                stop: function () {
                    var e = this._private;
                    return e.playing = !1, e.started = !1, e.stopped = !0, this
                },
                rewind: function () {
                    return this.progress(0)
                },
                fastforward: function () {
                    return this.progress(1)
                },
                time: function (e) {
                    var t = this._private;
                    return void 0 === e ? t.progress * t.duration : this.progress(e / t.duration)
                },
                progress: function (e) {
                    var t = this._private,
                        r = t.playing;
                    return void 0 === e ? t.progress : (r && this.pause(), t.progress = e, t.started = !1, r && this.play(), this)
                },
                completed: function () {
                    return 1 === this._private.progress
                },
                reverse: function () {
                    var e = this._private,
                        t = e.playing;
                    t && this.pause(), e.progress = 1 - e.progress, e.started = !1;
                    var r = function (t, r) {
                        var n = e[t];
                        e[t] = e[r], e[r] = n
                    };
                    r("zoom", "startZoom"), r("pan", "startPan"), r("position", "startPosition");
                    for (var n = 0; n < e.style.length; n++) {
                        var i = e.style[n],
                            a = i.name,
                            o = e.startStyle[a];
                        e.startStyle[a] = i, e.style[n] = o
                    }
                    return t && this.play(), this
                },
                promise: function (e) {
                    var t, r = this._private;
                    switch (e) {
                        case "frame":
                            t = r.frames;
                            break;
                        default:
                        case "complete":
                        case "completed":
                            t = r.completes
                    }
                    return new a(function (e, r) {
                        t.push(function () {
                            e()
                        })
                    })
                }
            }), s.complete = s.completed, t.exports = o
        }, {
            "./is": 83,
            "./promise": 86,
            "./util": 100
        }],
        3: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = {
                    aStar: function (e) {
                        var t = this;
                        e = e || {};
                        var r = function (e, t, n, i) {
                            if (e == t) return i.push(a.getElementById(t)), i;
                            if (t in n) {
                                var o = n[t],
                                    s = f[t];
                                return i.push(a.getElementById(t)), i.push(a.getElementById(s)), r(e, o, n, i)
                            }
                        },
                            i = function (e, t) {
                                if (0 !== e.length) {
                                    for (var r = 0, n = t[e[0]], i = 1; i < e.length; i++) {
                                        var a = t[e[i]];
                                        n > a && (n = a, r = i)
                                    }
                                    return r
                                }
                            },
                            a = this._private.cy;
                        if (null != e && null != e.root) {
                            var o = n.string(e.root) ? this.filter(e.root)[0] : e.root[0];
                            if (null != e.goal) {
                                var s = n.string(e.goal) ? this.filter(e.goal)[0] : e.goal[0];
                                if (null != e.heuristic && n.fn(e.heuristic)) var l = e.heuristic;
                                else var l = function () {
                                    return 0
                                };
                                if (null != e.weight && n.fn(e.weight)) var u = e.weight;
                                else var u = function (e) {
                                    return 1
                                };
                                if (null != e.directed) var c = e.directed;
                                else var c = !1;
                                var d = [],
                                    h = [o.id()],
                                    p = {},
                                    f = {},
                                    v = {},
                                    g = {};
                                v[o.id()] = 0, g[o.id()] = l(o);
                                for (var y = this.edges().stdFilter(function (e) {
                                    return !e.isLoop()
                                }), m = this.nodes(), b = 0; h.length > 0;) {
                                    var x = i(h, g),
                                        w = a.getElementById(h[x]);
                                    if (b++ , w.id() == s.id()) {
                                        var E = r(o.id(), s.id(), p, []);
                                        return E.reverse(), {
                                            found: !0,
                                            distance: v[w.id()],
                                            path: t.spawn(E),
                                            steps: b
                                        }
                                    }
                                    d.push(w.id()), h.splice(x, 1);
                                    var _ = w.connectedEdges();
                                    c && (_ = _.stdFilter(function (e) {
                                        return e.data("source") === w.id()
                                    })), _ = _.intersect(y);
                                    for (var S = 0; S < _.length; S++) {
                                        var P = _[S],
                                            T = P.connectedNodes().stdFilter(function (e) {
                                                return e.id() !== w.id()
                                            }).intersect(m);
                                        if (-1 == d.indexOf(T.id())) {
                                            var k = v[w.id()] + u.apply(P, [P]); - 1 != h.indexOf(T.id()) ? k < v[T.id()] && (v[T.id()] = k, g[T.id()] = k + l(T), p[T.id()] = w.id()) : (v[T.id()] = k, g[T.id()] = k + l(T), h.push(T.id()), p[T.id()] = w.id(), f[T.id()] = P.id())
                                        }
                                    }
                                }
                                return {
                                    found: !1,
                                    distance: void 0,
                                    path: void 0,
                                    steps: b
                                }
                            }
                        }
                    }
                };
            t.exports = i
        }, {
            "../../is": 83
        }],
        4: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = e("../../util"),
                a = {
                    bellmanFord: function (e) {
                        var t = this;
                        if (e = e || {}, null != e.weight && n.fn(e.weight)) var r = e.weight;
                        else var r = function (e) {
                            return 1
                        };
                        if (null != e.directed) var a = e.directed;
                        else var a = !1;
                        if (null != e.root) {
                            if (n.string(e.root)) var o = this.filter(e.root)[0];
                            else var o = e.root[0];
                            for (var s = this._private.cy, l = this.edges().stdFilter(function (e) {
                                return !e.isLoop()
                            }), u = this.nodes(), c = u.length, d = {}, h = 0; c > h; h++) d[u[h].id()] = h;
                            for (var p = [], f = [], v = [], h = 0; c > h; h++) u[h].id() === o.id() ? p[h] = 0 : p[h] = 1 / 0, f[h] = void 0;
                            for (var g = !1, h = 1; c > h; h++) {
                                g = !1;
                                for (var y = 0; y < l.length; y++) {
                                    var m = d[l[y].source().id()],
                                        b = d[l[y].target().id()],
                                        x = r.apply(l[y], [l[y]]),
                                        w = p[m] + x;
                                    if (w < p[b] && (p[b] = w, f[b] = m, v[b] = l[y], g = !0), !a) {
                                        var w = p[b] + x;
                                        w < p[m] && (p[m] = w, f[m] = b, v[m] = l[y], g = !0)
                                    }
                                }
                                if (!g) break
                            }
                            if (g)
                                for (var y = 0; y < l.length; y++) {
                                    var m = d[l[y].source().id()],
                                        b = d[l[y].target().id()],
                                        x = r.apply(l[y], [l[y]]);
                                    if (p[m] + x < p[b]) return i.error("Graph contains a negative weight cycle for Bellman-Ford"), {
                                        pathTo: void 0,
                                        distanceTo: void 0,
                                        hasNegativeWeightCycle: !0
                                    }
                                }
                            for (var E = [], h = 0; c > h; h++) E.push(u[h].id());
                            var _ = {
                                distanceTo: function (e) {
                                    if (n.string(e)) var t = s.filter(e)[0].id();
                                    else var t = e.id();
                                    return p[d[t]]
                                },
                                pathTo: function (e) {
                                    var r = function (e, t, r, n, i, a) {
                                        for (; ;) {
                                            if (i.push(s.getElementById(n[r])), i.push(a[r]), t === r) return i;
                                            var o = e[r];
                                            if ("undefined" == typeof o) return;
                                            r = o
                                        }
                                    };
                                    if (n.string(e)) var i = s.filter(e)[0].id();
                                    else var i = e.id();
                                    var a = [],
                                        l = r(f, d[o.id()], d[i], E, a, v);
                                    return null != l && l.reverse(), t.spawn(l)
                                },
                                hasNegativeWeightCycle: !1
                            };
                            return _
                        }
                    }
                };
            t.exports = a
        }, {
            "../../is": 83,
            "../../util": 100
        }],
        5: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = e("../../heap"),
                a = {
                    betweennessCentrality: function (e) {
                        e = e || {};
                        var t, r;
                        n.fn(e.weight) ? (r = e.weight, t = !0) : t = !1;
                        for (var a = null != e.directed ? e.directed : !1, o = this._private.cy, s = this.nodes(), l = {}, u = {}, c = 0, d = {
                            set: function (e, t) {
                                u[e] = t, t > c && (c = t)
                            },
                            get: function (e) {
                                return u[e]
                            }
                        }, h = 0; h < s.length; h++) {
                            var p = s[h],
                                f = p.id();
                            a ? l[f] = p.outgoers().nodes() : l[f] = p.openNeighborhood().nodes(), d.set(f, 0)
                        }
                        for (var v = 0; v < s.length; v++) {
                            for (var g = s[v].id(), y = [], m = {}, b = {}, x = {}, w = new i(function (e, t) {
                                return x[e] - x[t]
                            }), h = 0; h < s.length; h++) {
                                var f = s[h].id();
                                m[f] = [], b[f] = 0, x[f] = 1 / 0
                            }
                            for (b[g] = 1, x[g] = 0, w.push(g); !w.empty();) {
                                var p = w.pop();
                                if (y.push(p), t)
                                    for (var E = 0; E < l[p].length; E++) {
                                        var _, S = l[p][E],
                                            P = o.getElementById(p);
                                        _ = P.edgesTo(S).length > 0 ? P.edgesTo(S)[0] : S.edgesTo(P)[0];
                                        var T = r.apply(_, [_]);
                                        S = S.id(), x[S] > x[p] + T && (x[S] = x[p] + T, w.nodes.indexOf(S) < 0 ? w.push(S) : w.updateItem(S), b[S] = 0, m[S] = []), x[S] == x[p] + T && (b[S] = b[S] + b[p], m[S].push(p))
                                    } else
                                    for (var E = 0; E < l[p].length; E++) {
                                        var S = l[p][E].id();
                                        x[S] == 1 / 0 && (w.push(S), x[S] = x[p] + 1), x[S] == x[p] + 1 && (b[S] = b[S] + b[p], m[S].push(p))
                                    }
                            }
                            for (var k = {}, h = 0; h < s.length; h++) k[s[h].id()] = 0;
                            for (; y.length > 0;)
                                for (var S = y.pop(), E = 0; E < m[S].length; E++) {
                                    var p = m[S][E];
                                    k[p] = k[p] + b[p] / b[S] * (1 + k[S]), S != s[v].id() && d.set(S, d.get(S) + k[S])
                                }
                        }
                        var D = {
                            betweenness: function (e) {
                                if (n.string(e)) var e = o.filter(e).id();
                                else var e = e.id();
                                return d.get(e)
                            },
                            betweennessNormalized: function (e) {
                                if (0 == c) return 0;
                                if (n.string(e)) var e = o.filter(e).id();
                                else var e = e.id();
                                return d.get(e) / c
                            }
                        };
                        return D.betweennessNormalised = D.betweennessNormalized, D
                    }
                };
            a.bc = a.betweennessCentrality, t.exports = a
        }, {
            "../../heap": 81,
            "../../is": 83
        }],
        6: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = function (e) {
                    return e = {
                        bfs: e.bfs || !e.dfs,
                        dfs: e.dfs || !e.bfs
                    },
                        function (t, r, i) {
                            var a, o, s;
                            n.plainObject(t) && !n.elementOrCollection(t) && (a = t, t = a.roots || a.root, r = a.visit, i = a.directed, o = a.std, s = a.thisArg), i = 2 !== arguments.length || n.fn(r) ? i : r, r = n.fn(r) ? r : function () { };
                            for (var l, u = this._private.cy, c = t = n.string(t) ? this.filter(t) : t, d = [], h = [], p = {}, f = {}, v = {}, g = 0, y = this.nodes(), m = this.edges(), b = 0; b < c.length; b++) c[b].isNode() && (d.unshift(c[b]), e.bfs && (v[c[b].id()] = !0, h.push(c[b])), f[c[b].id()] = 0);
                            for (; 0 !== d.length;) {
                                var c = e.bfs ? d.shift() : d.pop();
                                if (e.dfs) {
                                    if (v[c.id()]) continue;
                                    v[c.id()] = !0, h.push(c)
                                }
                                var x, w = f[c.id()],
                                    E = p[c.id()],
                                    _ = null == E ? void 0 : E.connectedNodes().not(c)[0];
                                if (x = o ? r.call(s, c, E, _, g++, w) : r.call(c, g++, w, c, E, _), x === !0) {
                                    l = c;
                                    break
                                }
                                if (x === !1) break;
                                for (var S = c.connectedEdges(i ? function () {
                                    return this.data("source") === c.id()
                                } : void 0).intersect(m), b = 0; b < S.length; b++) {
                                    var P = S[b],
                                        T = P.connectedNodes(function () {
                                            return this.id() !== c.id()
                                        }).intersect(y);
                                    0 === T.length || v[T.id()] || (T = T[0], d.push(T), e.bfs && (v[T.id()] = !0, h.push(T)), p[T.id()] = P, f[T.id()] = f[c.id()] + 1)
                                }
                            }
                            for (var k = [], b = 0; b < h.length; b++) {
                                var D = h[b],
                                    C = p[D.id()];
                                C && k.push(C), k.push(D)
                            }
                            return {
                                path: u.collection(k, {
                                    unique: !0
                                }),
                                found: u.collection(l)
                            }
                        }
                },
                a = {
                    breadthFirstSearch: i({
                        bfs: !0
                    }),
                    depthFirstSearch: i({
                        dfs: !0
                    })
                };
            a.bfs = a.breadthFirstSearch, a.dfs = a.depthFirstSearch, t.exports = a
        }, {
            "../../is": 83
        }],
        7: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = {
                    closenessCentralityNormalized: function (e) {
                        e = e || {};
                        var t = this.cy(),
                            r = e.harmonic;
                        void 0 === r && (r = !0);
                        for (var i = {}, a = 0, o = this.nodes(), s = this.floydWarshall({
                            weight: e.weight,
                            directed: e.directed
                        }), l = 0; l < o.length; l++) {
                            for (var u = 0, c = 0; c < o.length; c++)
                                if (l != c) {
                                    var d = s.distance(o[l], o[c]);
                                    u += r ? 1 / d : d
                                }
                            r || (u = 1 / u), u > a && (a = u), i[o[l].id()] = u
                        }
                        return {
                            closeness: function (e) {
                                if (0 == a) return 0;
                                if (n.string(e)) var e = t.filter(e)[0].id();
                                else var e = e.id();
                                return i[e] / a
                            }
                        }
                    },
                    closenessCentrality: function (e) {
                        if (e = e || {}, null != e.root) {
                            if (n.string(e.root)) var t = this.filter(e.root)[0];
                            else var t = e.root[0];
                            if (null != e.weight && n.fn(e.weight)) var r = e.weight;
                            else var r = function () {
                                return 1
                            };
                            if (null != e.directed && n.bool(e.directed)) var i = e.directed;
                            else var i = !1;
                            var a = e.harmonic;
                            void 0 === a && (a = !0);
                            for (var o = this.dijkstra({
                                root: t,
                                weight: r,
                                directed: i
                            }), s = 0, l = this.nodes(), u = 0; u < l.length; u++)
                                if (l[u].id() != t.id()) {
                                    var c = o.distanceTo(l[u]);
                                    s += a ? 1 / c : c
                                }
                            return a ? s : 1 / s
                        }
                    }
                };
            i.cc = i.closenessCentrality, i.ccn = i.closenessCentralityNormalised = i.closenessCentralityNormalized, t.exports = i
        }, {
            "../../is": 83
        }],
        8: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = e("../../util"),
                a = {
                    degreeCentralityNormalized: function (e) {
                        e = e || {};
                        var t = this.cy();
                        if (null != e.directed) var r = e.directed;
                        else var r = !1;
                        var a = this.nodes(),
                            o = a.length;
                        if (r) {
                            for (var s = {}, l = {}, u = 0, c = 0, d = 0; o > d; d++) {
                                var h = a[d],
                                    p = this.degreeCentrality(i.extend({}, e, {
                                        root: h
                                    }));
                                u < p.indegree && (u = p.indegree), c < p.outdegree && (c = p.outdegree), s[h.id()] = p.indegree, l[h.id()] = p.outdegree
                            }
                            return {
                                indegree: function (e) {
                                    if (0 == u) return 0;
                                    if (n.string(e)) var e = t.filter(e)[0].id();
                                    else var e = e.id();
                                    return s[e] / u
                                },
                                outdegree: function (e) {
                                    if (0 == c) return 0;
                                    if (n.string(e)) var e = t.filter(e)[0].id();
                                    else var e = e.id();
                                    return l[e] / c
                                }
                            }
                        }
                        for (var f = {}, v = 0, d = 0; o > d; d++) {
                            var h = a[d],
                                p = this.degreeCentrality(i.extend({}, e, {
                                    root: h
                                }));
                            v < p.degree && (v = p.degree), f[h.id()] = p.degree
                        }
                        return {
                            degree: function (e) {
                                if (0 == v) return 0;
                                if (n.string(e)) var e = t.filter(e)[0].id();
                                else var e = e.id();
                                return f[e] / v
                            }
                        }
                    },
                    degreeCentrality: function (e) {
                        e = e || {};
                        var t = this;
                        if (null != e && null != e.root) {
                            var r = n.string(e.root) ? this.filter(e.root)[0] : e.root[0];
                            if (null != e.weight && n.fn(e.weight)) var i = e.weight;
                            else var i = function (e) {
                                return 1
                            };
                            if (null != e.directed) var a = e.directed;
                            else var a = !1;
                            if (null != e.alpha && n.number(e.alpha)) var o = e.alpha;
                            else o = 0;
                            if (a) {
                                for (var s = r.connectedEdges('edge[target = "' + r.id() + '"]').intersection(t), l = r.connectedEdges('edge[source = "' + r.id() + '"]').intersection(t), u = s.length, c = l.length, d = 0, h = 0, p = 0; p < s.length; p++) {
                                    var f = s[p];
                                    d += i.apply(f, [f])
                                }
                                for (var p = 0; p < l.length; p++) {
                                    var f = l[p];
                                    h += i.apply(f, [f])
                                }
                                return {
                                    indegree: Math.pow(u, 1 - o) * Math.pow(d, o),
                                    outdegree: Math.pow(c, 1 - o) * Math.pow(h, o)
                                }
                            }
                            for (var v = r.connectedEdges().intersection(t), g = v.length, y = 0, p = 0; p < v.length; p++) {
                                var f = v[p];
                                y += i.apply(f, [f])
                            }
                            return {
                                degree: Math.pow(g, 1 - o) * Math.pow(y, o)
                            }
                        }
                    }
                };
            a.dc = a.degreeCentrality, a.dcn = a.degreeCentralityNormalised = a.degreeCentralityNormalized, t.exports = a
        }, {
            "../../is": 83,
            "../../util": 100
        }],
        9: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = e("../../heap"),
                a = {
                    dijkstra: function (e, t, r) {
                        var a;
                        n.plainObject(e) && !n.elementOrCollection(e) && (a = e, e = a.root, t = a.weight, r = a.directed);
                        var o = this._private.cy;
                        t = n.fn(t) ? t : function () {
                            return 1
                        };
                        for (var s = n.string(e) ? this.filter(e)[0] : e[0], l = {}, u = {}, c = {}, d = this.edges().filter(function () {
                            return !this.isLoop()
                        }), h = this.nodes(), p = function (e) {
                            return l[e.id()]
                        }, f = function (e, t) {
                            l[e.id()] = t, v.updateItem(e)
                        }, v = new i(function (e, t) {
                            return p(e) - p(t)
                        }), g = 0; g < h.length; g++) {
                            var y = h[g];
                            l[y.id()] = y.same(s) ? 0 : 1 / 0, v.push(y)
                        }
                        for (var m = function (e, n) {
                            for (var i, a = (r ? e.edgesTo(n) : e.edgesWith(n)).intersect(d), o = 1 / 0, s = 0; s < a.length; s++) {
                                var l = a[s],
                                    u = t.apply(l, [l]);
                                (o > u || !i) && (o = u, i = l)
                            }
                            return {
                                edge: i,
                                dist: o
                            }
                        }; v.size() > 0;) {
                            var b = v.pop(),
                                x = p(b),
                                w = b.id();
                            if (c[w] = x, x === Math.Infinite) break;
                            for (var E = b.neighborhood().intersect(h), g = 0; g < E.length; g++) {
                                var _ = E[g],
                                    S = _.id(),
                                    P = m(b, _),
                                    T = x + P.dist;
                                T < p(_) && (f(_, T), u[S] = {
                                    node: b,
                                    edge: P.edge
                                })
                            }
                        }
                        return {
                            distanceTo: function (e) {
                                var t = n.string(e) ? h.filter(e)[0] : e[0];
                                return c[t.id()]
                            },
                            pathTo: function (e) {
                                var t = n.string(e) ? h.filter(e)[0] : e[0],
                                    r = [],
                                    i = t;
                                if (t.length > 0)
                                    for (r.unshift(t); u[i.id()];) {
                                        var a = u[i.id()];
                                        r.unshift(a.edge), r.unshift(a.node), i = a.node
                                    }
                                return o.collection(r)
                            }
                        }
                    }
                };
            t.exports = a
        }, {
            "../../heap": 81,
            "../../is": 83
        }],
        10: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = {
                    floydWarshall: function (e) {
                        e = e || {};
                        var t = this.cy();
                        if (null != e.weight && n.fn(e.weight)) var r = e.weight;
                        else var r = function (e) {
                            return 1
                        };
                        if (null != e.directed) var i = e.directed;
                        else var i = !1;
                        for (var a = this.edges().stdFilter(function (e) {
                            return !e.isLoop()
                        }), o = this.nodes(), s = o.length, l = {}, u = 0; s > u; u++) l[o[u].id()] = u;
                        for (var c = [], u = 0; s > u; u++) {
                            for (var d = new Array(s), h = 0; s > h; h++) u == h ? d[h] = 0 : d[h] = 1 / 0;
                            c.push(d)
                        }
                        var p = [],
                            f = [],
                            v = function (e) {
                                for (var t = 0; s > t; t++) {
                                    for (var r = new Array(s), n = 0; s > n; n++) r[n] = void 0;
                                    e.push(r)
                                }
                            };
                        v(p), v(f);
                        for (var u = 0; u < a.length; u++) {
                            var g = l[a[u].source().id()],
                                y = l[a[u].target().id()],
                                m = r.apply(a[u], [a[u]]);
                            c[g][y] > m && (c[g][y] = m, p[g][y] = y, f[g][y] = a[u])
                        }
                        if (!i)
                            for (var u = 0; u < a.length; u++) {
                                var g = l[a[u].target().id()],
                                    y = l[a[u].source().id()],
                                    m = r.apply(a[u], [a[u]]);
                                c[g][y] > m && (c[g][y] = m, p[g][y] = y, f[g][y] = a[u])
                            }
                        for (var b = 0; s > b; b++)
                            for (var u = 0; s > u; u++)
                                for (var h = 0; s > h; h++) c[u][b] + c[b][h] < c[u][h] && (c[u][h] = c[u][b] + c[b][h], p[u][h] = p[u][b]);
                        for (var x = [], u = 0; s > u; u++) x.push(o[u].id());
                        var w = {
                            distance: function (e, r) {
                                if (n.string(e)) var i = t.filter(e)[0].id();
                                else var i = e.id();
                                if (n.string(r)) var a = t.filter(r)[0].id();
                                else var a = r.id();
                                return c[l[i]][l[a]]
                            },
                            path: function (e, r) {
                                var i = function (e, r, n, i, a) {
                                    if (e === r) return t.getElementById(i[e]);
                                    if (void 0 !== n[e][r]) {
                                        for (var o = [t.getElementById(i[e])], s = e; e !== r;) {
                                            s = e, e = n[e][r];
                                            var l = a[s][e];
                                            o.push(l), o.push(t.getElementById(i[e]))
                                        }
                                        return o
                                    }
                                };
                                if (n.string(e)) var a = t.filter(e)[0].id();
                                else var a = e.id();
                                if (n.string(r)) var o = t.filter(r)[0].id();
                                else var o = r.id();
                                var s = i(l[a], l[o], p, x, f);
                                return t.collection(s)
                            }
                        };
                        return w
                    }
                };
            t.exports = i
        }, {
            "../../is": 83
        }],
        11: [function (e, t, r) {
            "use strict";
            var n = e("../../util"),
                i = {};
            [e("./bfs-dfs"), e("./dijkstra"), e("./kruskal"), e("./a-star"), e("./floyd-warshall"), e("./bellman-ford"), e("./kerger-stein"), e("./page-rank"), e("./degree-centrality"), e("./closeness-centrality"), e("./betweenness-centrality")].forEach(function (e) {
                n.extend(i, e)
            }), t.exports = i
        }, {
            "../../util": 100,
            "./a-star": 3,
            "./bellman-ford": 4,
            "./betweenness-centrality": 5,
            "./bfs-dfs": 6,
            "./closeness-centrality": 7,
            "./degree-centrality": 8,
            "./dijkstra": 9,
            "./floyd-warshall": 10,
            "./kerger-stein": 12,
            "./kruskal": 13,
            "./page-rank": 14
        }],
        12: [function (e, t, r) {
            "use strict";
            var n = e("../../util"),
                i = {
                    kargerStein: function (e) {
                        var t = this;
                        e = e || {};
                        var r = function (e, t, r) {
                            for (var n = r[e], i = n[1], a = n[2], o = t[i], s = t[a], l = r.filter(function (e) {
                                return t[e[1]] === o && t[e[2]] === s ? !1 : t[e[1]] !== s || t[e[2]] !== o
                            }), u = 0; u < l.length; u++) {
                                var c = l[u];
                                c[1] === s ? (l[u] = c.slice(0), l[u][1] = o) : c[2] === s && (l[u] = c.slice(0), l[u][2] = o)
                            }
                            for (var u = 0; u < t.length; u++) t[u] === s && (t[u] = o);
                            return l
                        },
                            i = function (e, t, n, a) {
                                if (a >= n) return t;
                                var o = Math.floor(Math.random() * t.length),
                                    s = r(o, e, t);
                                return i(e, s, n - 1, a)
                            },
                            a = this._private.cy,
                            o = this.edges().stdFilter(function (e) {
                                return !e.isLoop()
                            }),
                            s = this.nodes(),
                            l = s.length,
                            u = o.length,
                            c = Math.ceil(Math.pow(Math.log(l) / Math.LN2, 2)),
                            d = Math.floor(l / Math.sqrt(2));
                        if (2 > l) return void n.error("At least 2 nodes are required for Karger-Stein algorithm");
                        for (var h = {}, p = 0; l > p; p++) h[s[p].id()] = p;
                        for (var f = [], p = 0; u > p; p++) {
                            var v = o[p];
                            f.push([p, h[v.source().id()], h[v.target().id()]])
                        }
                        for (var g, y = 1 / 0, m = [], p = 0; l > p; p++) m.push(p);
                        for (var b = 0; c >= b; b++) {
                            var x = m.slice(0),
                                w = i(x, f, l, d),
                                E = x.slice(0),
                                _ = i(x, w, d, 2),
                                S = i(E, w, d, 2);
                            _.length <= S.length && _.length < y ? (y = _.length, g = [_, x]) : S.length <= _.length && S.length < y && (y = S.length, g = [S, E])
                        }
                        for (var P = g[0].map(function (e) {
                            return o[e[0]]
                        }), T = [], k = [], D = g[1][0], p = 0; p < g[1].length; p++) {
                            var C = g[1][p];
                            C === D ? T.push(s[p]) : k.push(s[p])
                        }
                        var M = {
                            cut: t.spawn(a, P),
                            partition1: t.spawn(T),
                            partition2: t.spawn(k)
                        };
                        return M
                    }
                };
            t.exports = i
        }, {
            "../../util": 100
        }],
        13: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = {
                    kruskal: function (e) {
                        function t(e) {
                            for (var t = 0; t < a.length; t++) {
                                var r = a[t];
                                if (r.anySame(e)) return {
                                    eles: r,
                                    index: t
                                }
                            }
                        }
                        var r = this.cy();
                        e = n.fn(e) ? e : function () {
                            return 1
                        };
                        for (var i = r.collection(r, []), a = [], o = this.nodes(), s = 0; s < o.length; s++) a.push(o[s].collection());
                        for (var l = this.edges(), u = l.toArray().sort(function (t, r) {
                            var n = e.call(t, t),
                                i = e.call(r, r);
                            return n - i
                        }), s = 0; s < u.length; s++) {
                            var c = u[s],
                                d = c.source()[0],
                                h = c.target()[0],
                                p = t(d),
                                f = t(h);
                            p.index !== f.index && (i = i.add(c), a[p.index] = p.eles.add(f.eles), a.splice(f.index, 1))
                        }
                        return o.add(i)
                    }
                };
            t.exports = i
        }, {
            "../../is": 83
        }],
        14: [function (e, t, r) {
            "use strict";
            var n = e("../../is"),
                i = {
                    pageRank: function (e) {
                        e = e || {};
                        var t = function (e) {
                            for (var t = e.length, r = 0, n = 0; t > n; n++) r += e[n];
                            for (var n = 0; t > n; n++) e[n] = e[n] / r
                        };
                        if (null != e && null != e.dampingFactor) var r = e.dampingFactor;
                        else var r = .8;
                        if (null != e && null != e.precision) var i = e.precision;
                        else var i = 1e-6;
                        if (null != e && null != e.iterations) var a = e.iterations;
                        else var a = 200;
                        if (null != e && null != e.weight && n.fn(e.weight)) var o = e.weight;
                        else var o = function (e) {
                            return 1
                        };
                        for (var s = this._private.cy, l = this.edges().stdFilter(function (e) {
                            return !e.isLoop()
                        }), u = this.nodes(), c = u.length, d = l.length, h = {}, p = 0; c > p; p++) h[u[p].id()] = p;
                        for (var f = [], v = [], g = (1 - r) / c, p = 0; c > p; p++) {
                            for (var y = [], m = 0; c > m; m++) y.push(0);
                            f.push(y), v.push(0)
                        }
                        for (var p = 0; d > p; p++) {
                            var b = l[p],
                                x = h[b.source().id()],
                                w = h[b.target().id()],
                                E = o.apply(b, [b]);
                            f[w][x] += E, v[x] += E
                        }
                        for (var _ = 1 / c + g, m = 0; c > m; m++)
                            if (0 === v[m])
                                for (var p = 0; c > p; p++) f[p][m] = _;
                            else
                                for (var p = 0; c > p; p++) f[p][m] = f[p][m] / v[m] + g;
                        for (var S, P = [], T = [], p = 0; c > p; p++) P.push(1), T.push(0);
                        for (var k = 0; a > k; k++) {
                            for (var D = T.slice(0), p = 0; c > p; p++)
                                for (var m = 0; c > m; m++) D[p] += f[p][m] * P[m];
                            t(D), S = P, P = D;
                            for (var C = 0, p = 0; c > p; p++) C += Math.pow(S[p] - P[p], 2);
                            if (i > C) break
                        }
                        var M = {
                            rank: function (e) {
                                if (n.string(e)) var t = s.filter(e)[0].id();
                                else var t = e.id();
                                return P[h[t]]
                            }
                        };
                        return M
                    }
                };
            t.exports = i
        }, {
            "../../is": 83
        }],
        15: [function (e, t, r) {
            "use strict";
            var n = e("../define"),
                i = {
                    animate: n.animate(),
                    animation: n.animation(),
                    animated: n.animated(),
                    clearQueue: n.clearQueue(),
                    delay: n.delay(),
                    delayAnimation: n.delayAnimation(),
                    stop: n.stop()
                };
            t.exports = i
        }, {
            "../define": 44
        }],
        16: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = {
                    classes: function (e) {
                        e = (e || "").match(/\S+/g) || [];
                        for (var t = this, r = [], i = {}, a = 0; a < e.length; a++) {
                            var o = e[a];
                            i[o] = !0
                        }
                        for (var s = 0; s < t.length; s++) {
                            for (var l = t[s], u = l._private, c = u.classes, d = !1, a = 0; a < e.length; a++) {
                                var o = e[a],
                                    h = c[o];
                                if (!h) {
                                    d = !0;
                                    break
                                }
                            }
                            if (!d)
                                for (var e = Object.keys(c), a = 0; a < e.length; a++) {
                                    var p = e[a],
                                        h = c[p],
                                        f = i[p];
                                    if (h && !f) {
                                        d = !0;
                                        break
                                    }
                                }
                            d && (u.classes = n.copy(i), r.push(l))
                        }
                        return r.length > 0 && this.spawn(r).updateStyle().trigger("class"), t
                    },
                    addClass: function (e) {
                        return this.toggleClass(e, !0)
                    },
                    hasClass: function (e) {
                        var t = this[0];
                        return !(null == t || !t._private.classes[e])
                    },
                    toggleClass: function (e, t) {
                        for (var r = e.match(/\S+/g) || [], n = this, i = [], a = 0, o = n.length; o > a; a++)
                            for (var s = n[a], l = !1, u = 0; u < r.length; u++) {
                                var c = r[u],
                                    d = s._private.classes,
                                    h = d[c],
                                    p = t || void 0 === t && !h;
                                p ? (d[c] = !0, h || l || (i.push(s), l = !0)) : (d[c] = !1, h && !l && (i.push(s), l = !0))
                            }
                        return i.length > 0 && this.spawn(i).updateStyle().trigger("class"), n
                    },
                    removeClass: function (e) {
                        return this.toggleClass(e, !1)
                    },
                    flashClass: function (e, t) {
                        var r = this;
                        if (null == t) t = 250;
                        else if (0 === t) return r;
                        return r.addClass(e), setTimeout(function () {
                            r.removeClass(e)
                        }, t), r
                    }
                };
            t.exports = i
        }, {
            "../util": 100
        }],
        17: [function (e, t, r) {
            "use strict";
            var n = {
                allAre: function (e) {
                    return this.filter(e).length === this.length
                },
                is: function (e) {
                    return this.filter(e).length > 0
                },
                some: function (e, t) {
                    for (var r = 0; r < this.length; r++) {
                        var n = t ? e.apply(t, [this[r], r, this]) : e(this[r], r, this);
                        if (n) return !0
                    }
                    return !1
                },
                every: function (e, t) {
                    for (var r = 0; r < this.length; r++) {
                        var n = t ? e.apply(t, [this[r], r, this]) : e(this[r], r, this);
                        if (!n) return !1
                    }
                    return !0
                },
                same: function (e) {
                    return e = this.cy().collection(e), this.length !== e.length ? !1 : this.intersect(e).length === this.length
                },
                anySame: function (e) {
                    return e = this.cy().collection(e), this.intersect(e).length > 0
                },
                allAreNeighbors: function (e) {
                    return e = this.cy().collection(e), this.neighborhood().intersect(e).length === e.length
                }
            };
            n.allAreNeighbours = n.allAreNeighbors, t.exports = n
        }, {}],
        18: [function (e, t, r) {
            "use strict";
            var n = {
                parent: function (e) {
                    for (var t = [], r = this._private.cy, n = 0; n < this.length; n++) {
                        var i = this[n],
                            a = r.getElementById(i._private.data.parent);
                        a.size() > 0 && t.push(a)
                    }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                },
                parents: function (e) {
                    for (var t = [], r = this.parent(); r.nonempty();) {
                        for (var n = 0; n < r.length; n++) {
                            var i = r[n];
                            t.push(i)
                        }
                        r = r.parent()
                    }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                },
                commonAncestors: function (e) {
                    for (var t, r = 0; r < this.length; r++) {
                        var n = this[r],
                            i = n.parents();
                        t = t || i, t = t.intersect(i)
                    }
                    return t.filter(e)
                },
                orphans: function (e) {
                    return this.stdFilter(function (e) {
                        return e.isNode() && e.parent().empty()
                    }).filter(e)
                },
                nonorphans: function (e) {
                    return this.stdFilter(function (e) {
                        return e.isNode() && e.parent().nonempty()
                    }).filter(e)
                },
                children: function (e) {
                    for (var t = [], r = 0; r < this.length; r++) {
                        var n = this[r];
                        t = t.concat(n._private.children)
                    }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                },
                siblings: function (e) {
                    return this.parent().children().not(this).filter(e)
                },
                isParent: function () {
                    var e = this[0];
                    return e ? 0 !== e._private.children.length : void 0
                },
                isChild: function () {
                    var e = this[0];
                    return e ? void 0 !== e._private.data.parent && 0 !== e.parent().length : void 0
                },
                descendants: function (e) {
                    function t(e) {
                        for (var n = 0; n < e.length; n++) {
                            var i = e[n];
                            r.push(i), i.children().nonempty() && t(i.children())
                        }
                    }
                    var r = [];
                    return t(this.children()), this.spawn(r, {
                        unique: !0
                    }).filter(e)
                }
            };
            n.ancestors = n.parents, t.exports = n
        }, {}],
        19: [function (e, t, r) {
            "use strict";
            var n, i, a = e("../define");
            n = i = {
                data: a.data({
                    field: "data",
                    bindingEvent: "data",
                    allowBinding: !0,
                    allowSetting: !0,
                    settingEvent: "data",
                    settingTriggersEvent: !0,
                    triggerFnName: "trigger",
                    allowGetting: !0,
                    immutableKeys: {
                        id: !0,
                        source: !0,
                        target: !0,
                        parent: !0
                    },
                    updateStyle: !0
                }),
                removeData: a.removeData({
                    field: "data",
                    event: "data",
                    triggerFnName: "trigger",
                    triggerEvent: !0,
                    immutableKeys: {
                        id: !0,
                        source: !0,
                        target: !0,
                        parent: !0
                    },
                    updateStyle: !0
                }),
                scratch: a.data({
                    field: "scratch",
                    bindingEvent: "scratch",
                    allowBinding: !0,
                    allowSetting: !0,
                    settingEvent: "scratch",
                    settingTriggersEvent: !0,
                    triggerFnName: "trigger",
                    allowGetting: !0,
                    updateStyle: !0
                }),
                removeScratch: a.removeData({
                    field: "scratch",
                    event: "scratch",
                    triggerFnName: "trigger",
                    triggerEvent: !0,
                    updateStyle: !0
                }),
                rscratch: a.data({
                    field: "rscratch",
                    allowBinding: !1,
                    allowSetting: !0,
                    settingTriggersEvent: !1,
                    allowGetting: !0
                }),
                removeRscratch: a.removeData({
                    field: "rscratch",
                    triggerEvent: !1
                }),
                id: function () {
                    var e = this[0];
                    return e ? e._private.data.id : void 0
                }
            }, n.attr = n.data, n.removeAttr = n.removeData, t.exports = i
        }, {
            "../define": 44
        }],
        20: [function (e, t, r) {
            "use strict";

            function n(e) {
                return function (t) {
                    var r = this;
                    if (void 0 === t && (t = !0), 0 !== r.length && r.isNode() && !r.removed()) {
                        for (var n = 0, i = r[0], a = i._private.edges, o = 0; o < a.length; o++) {
                            var s = a[o];
                            !t && s.isLoop() || (n += e(i, s))
                        }
                        return n
                    }
                }
            }

            function i(e, t) {
                return function (r) {
                    for (var n, i = this.nodes(), a = 0; a < i.length; a++) {
                        var o = i[a],
                            s = o[e](r);
                        void 0 === s || void 0 !== n && !t(s, n) || (n = s)
                    }
                    return n
                }
            }
            var a = e("../util"),
                o = {};
            a.extend(o, {
                degree: n(function (e, t) {
                    return t.source().same(t.target()) ? 2 : 1
                }),
                indegree: n(function (e, t) {
                    return t.target().same(e) ? 1 : 0
                }),
                outdegree: n(function (e, t) {
                    return t.source().same(e) ? 1 : 0
                })
            }), a.extend(o, {
                minDegree: i("degree", function (e, t) {
                    return t > e
                }),
                maxDegree: i("degree", function (e, t) {
                    return e > t
                }),
                minIndegree: i("indegree", function (e, t) {
                    return t > e
                }),
                maxIndegree: i("indegree", function (e, t) {
                    return e > t
                }),
                minOutdegree: i("outdegree", function (e, t) {
                    return t > e
                }),
                maxOutdegree: i("outdegree", function (e, t) {
                    return e > t
                })
            }), a.extend(o, {
                totalDegree: function (e) {
                    for (var t = 0, r = this.nodes(), n = 0; n < r.length; n++) t += r[n].degree(e);
                    return t
                }
            }), t.exports = o
        }, {
            "../util": 100
        }],
        21: [function (e, t, r) {
            "use strict";

            function n(e) {
                return {
                    includeNodes: l["default"](e.includeNodes, x.includeNodes),
                    includeEdges: l["default"](e.includeEdges, x.includeEdges),
                    includeLabels: l["default"](e.includeLabels, x.includeLabels),
                    includeShadows: l["default"](e.includeShadows, x.includeShadows),
                    includeOverlays: l["default"](e.includeOverlays, x.includeOverlays),
                    useCache: l["default"](e.useCache, x.useCache)
                }
            }
            var i, a, o = e("../define"),
                s = e("../is"),
                l = e("../util"),
                u = e("../math");
            i = a = {
                position: o.data({
                    field: "position",
                    bindingEvent: "position",
                    allowBinding: !0,
                    allowSetting: !0,
                    settingEvent: "position",
                    settingTriggersEvent: !0,
                    triggerFnName: "rtrigger",
                    allowGetting: !0,
                    validKeys: ["x", "y"],
                    onSet: function (e) {
                        var t = e.updateCompoundBounds();
                        t.rtrigger("position")
                    },
                    canSet: function (e) {
                        return !e.locked() && !e.isParent()
                    }
                }),
                silentPosition: o.data({
                    field: "position",
                    bindingEvent: "position",
                    allowBinding: !1,
                    allowSetting: !0,
                    settingEvent: "position",
                    settingTriggersEvent: !1,
                    triggerFnName: "trigger",
                    allowGetting: !0,
                    validKeys: ["x", "y"],
                    onSet: function (e) {
                        e.updateCompoundBounds()
                    },
                    canSet: function (e) {
                        return !e.locked() && !e.isParent()
                    }
                }),
                positions: function (e, t) {
                    if (s.plainObject(e)) this.position(e);
                    else if (s.fn(e)) {
                        for (var r = e, n = 0; n < this.length; n++) {
                            var i = this[n],
                                e = r.apply(i, [n, i]);
                            if (e && !i.locked() && !i.isParent()) {
                                var a = i._private.position;
                                a.x = e.x, a.y = e.y
                            }
                        }
                        var o = this.updateCompoundBounds(),
                            l = o.length > 0 ? this.add(o) : this;
                        t ? l.trigger("position") : l.rtrigger("position")
                    }
                    return this
                },
                silentPositions: function (e) {
                    return this.positions(e, !0)
                },
                renderedPosition: function (e, t) {
                    var r = this[0],
                        n = this.cy(),
                        i = n.zoom(),
                        a = n.pan(),
                        o = s.plainObject(e) ? e : void 0,
                        l = void 0 !== o || void 0 !== t && s.string(e);
                    if (r && r.isNode()) {
                        if (!l) {
                            var u = r._private.position;
                            return o = {
                                x: u.x * i + a.x,
                                y: u.y * i + a.y
                            }, void 0 === e ? o : o[e]
                        }
                        for (var c = 0; c < this.length; c++) {
                            var r = this[c];
                            void 0 !== t ? r._private.position[e] = (t - a[e]) / i : void 0 !== o && (r._private.position = {
                                x: (o.x - a.x) / i,
                                y: (o.y - a.y) / i
                            })
                        }
                        this.rtrigger("position")
                    } else if (!l) return;
                    return this
                },
                relativePosition: function (e, t) {
                    var r = this[0],
                        n = this.cy(),
                        i = s.plainObject(e) ? e : void 0,
                        a = void 0 !== i || void 0 !== t && s.string(e),
                        o = n.hasCompoundNodes();
                    if (r && r.isNode()) {
                        if (!a) {
                            var l = r._private.position,
                                u = o ? r.parent() : null,
                                c = u && u.length > 0,
                                d = c;
                            c && (u = u[0]);
                            var h = d ? u._private.position : {
                                x: 0,
                                y: 0
                            };
                            return i = {
                                x: l.x - h.x,
                                y: l.y - h.y
                            }, void 0 === e ? i : i[e]
                        }
                        for (var p = 0; p < this.length; p++) {
                            var r = this[p],
                                u = o ? r.parent() : null,
                                c = u && u.length > 0,
                                d = c;
                            c && (u = u[0]);
                            var h = d ? u._private.position : {
                                x: 0,
                                y: 0
                            };
                            void 0 !== t ? r._private.position[e] = t + h[e] : void 0 !== i && (r._private.position = {
                                x: i.x + h.x,
                                y: i.y + h.y
                            })
                        }
                        this.rtrigger("position")
                    } else if (!a) return;
                    return this
                },
                renderedBoundingBox: function (e) {
                    var t = this.boundingBox(e),
                        r = this.cy(),
                        n = r.zoom(),
                        i = r.pan(),
                        a = t.x1 * n + i.x,
                        o = t.x2 * n + i.x,
                        s = t.y1 * n + i.y,
                        l = t.y2 * n + i.y;
                    return {
                        x1: a,
                        x2: o,
                        y1: s,
                        y2: l,
                        w: o - a,
                        h: l - s
                    }
                },
                updateCompoundBounds: function () {
                    function e(e) {
                        if (e.isParent()) {
                            var t = e._private,
                                n = e.children(),
                                i = "include" === e.pstyle("compound-sizing-wrt-labels").value,
                                a = n.boundingBox({
                                    includeLabels: i,
                                    includeShadows: !1,
                                    includeOverlays: !1,
                                    useCache: !1
                                }),
                                o = t.position;
                            t.autoWidth = a.w, o.x = (a.x1 + a.x2) / 2, t.autoHeight = a.h, o.y = (a.y1 + a.y2) / 2, r.push(e)
                        }
                    }
                    var t = this.cy();
                    if (!t.styleEnabled() || !t.hasCompoundNodes()) return t.collection();
                    for (var r = [], n = this; n.nonempty();) {
                        for (var i = 0; i < n.length; i++) {
                            var a = n[i];
                            e(a)
                        }
                        n = n.parent()
                    }
                    return this.spawn(r)
                }
            };
            var c = function (e) {
                return e === 1 / 0 || e === -(1 / 0) ? 0 : e
            },
                d = function (e, t, r, n, i) {
                    n - t !== 0 && i - r !== 0 && (e.x1 = t < e.x1 ? t : e.x1, e.x2 = n > e.x2 ? n : e.x2, e.y1 = r < e.y1 ? r : e.y1, e.y2 = i > e.y2 ? i : e.y2)
                },
                h = function (e, t) {
                    return d(e, t.x1, t.y1, t.x2, t.y2)
                },
                p = function (e, t, r) {
                    return l.getPrefixedProperty(e, t, r)
                },
                f = function (e, t, r, n) {
                    var i, a, o = t._private,
                        s = o.rstyle,
                        l = s.arrowWidth / 2,
                        u = t.pstyle(r + "-arrow-shape").value;
                    "none" !== u && ("source" === r ? (i = s.srcX, a = s.srcY) : "target" === r ? (i = s.tgtX, a = s.tgtY) : (i = s.midX, a = s.midY), d(e, i - l, a - l, i + l, a + l))
                },
                v = function (e, t, r, n) {
                    var i;
                    i = r ? r + "-" : "";
                    var a = t._private,
                        o = a.rstyle,
                        s = t.pstyle(i + "label").strValue;
                    if (s) {
                        var l, u, c, h, f = t.pstyle("text-halign"),
                            v = t.pstyle("text-valign"),
                            g = p(o, "labelWidth", r),
                            y = p(o, "labelHeight", r),
                            m = p(o, "labelX", r),
                            b = p(o, "labelY", r),
                            x = t.pstyle(i + "text-margin-x").pfValue,
                            w = t.pstyle(i + "text-margin-y").pfValue,
                            E = t.isEdge(),
                            _ = t.pstyle(i + "text-rotation"),
                            S = t.pstyle("text-shadow-blur").pfValue / 2,
                            P = t.pstyle("text-shadow-offset-x").pfValue,
                            T = t.pstyle("text-shadow-offset-y").pfValue,
                            k = t.pstyle("text-shadow-opacity").value,
                            D = t.pstyle("text-outline-width").pfValue,
                            C = t.pstyle("text-border-width").pfValue,
                            M = C / 2,
                            N = y,
                            B = g,
                            z = B / 2,
                            I = N / 2;
                        if (E) l = m - z, u = m + z, c = b - I, h = b + I;
                        else {
                            switch (f.value) {
                                case "left":
                                    l = m - B, u = m;
                                    break;
                                case "center":
                                    l = m - z, u = m + z;
                                    break;
                                case "right":
                                    l = m, u = m + B
                            }
                            switch (v.value) {
                                case "top":
                                    c = b - N, h = b;
                                    break;
                                case "center":
                                    c = b - I, h = b + I;
                                    break;
                                case "bottom":
                                    c = b, h = b + N
                            }
                        }
                        var L = E && "autorotate" === _.strValue,
                            O = null != _.pfValue && 0 !== _.pfValue;
                        if (L || O) {
                            var A = L ? p(a.rstyle, "labelAngle", r) : _.pfValue,
                                R = Math.cos(A),
                                q = Math.sin(A),
                                V = function (e, t) {
                                    return e -= m, t -= b, {
                                        x: e * R - t * q + m,
                                        y: e * q + t * R + b
                                    }
                                },
                                F = V(l, c),
                                j = V(l, h),
                                X = V(u, c),
                                Y = V(u, h);
                            l = Math.min(F.x, j.x, X.x, Y.x), u = Math.max(F.x, j.x, X.x, Y.x), c = Math.min(F.y, j.y, X.y, Y.y), h = Math.max(F.y, j.y, X.y, Y.y)
                        }
                        l += x - Math.max(D, M), u += x + Math.max(D, M), c += w - Math.max(D, M), h += w + Math.max(D, M), d(e, l, c, u, h), n.includeShadows && k > 0 && (l += -S + P, u += +S + P, c += -S + T, h += +S + T, d(e, l, c, u, h))
                    }
                    return e
                },
                g = function (e, t) {
                    var r, n, i, a, o, s, l = e._private.cy,
                        h = l._private,
                        p = h.styleEnabled,
                        g = {
                            x1: 1 / 0,
                            y1: 1 / 0,
                            x2: -(1 / 0),
                            y2: -(1 / 0)
                        },
                        y = e._private,
                        m = p ? e.pstyle("display").value : "element",
                        b = e.isNode(),
                        x = e.isEdge(),
                        w = "none" !== m;
                    if (w) {
                        var E = 0,
                            _ = 0;
                        p && t.includeOverlays && (E = e.pstyle("overlay-opacity").value, 0 !== E && (_ = e.pstyle("overlay-padding").value));
                        var S = 0,
                            P = 0;
                        if (p && (S = e.pstyle("width").pfValue, P = S / 2), b && t.includeNodes) {
                            var T = y.position;
                            o = T.x, s = T.y;
                            var S = e.outerWidth(),
                                k = S / 2,
                                D = e.outerHeight(),
                                C = D / 2;
                            r = o - k - _, n = o + k + _, i = s - C - _, a = s + C + _, d(g, r, i, n, a)
                        } else if (x && t.includeEdges) {
                            var M = y.rstyle || {};
                            if (p && (r = Math.min(M.srcX, M.midX, M.tgtX), n = Math.max(M.srcX, M.midX, M.tgtX), i = Math.min(M.srcY, M.midY, M.tgtY), a = Math.max(M.srcY, M.midY, M.tgtY), r -= P, n += P, i -= P, a += P, d(g, r, i, n, a)), p && "haystack" === e.pstyle("curve-style").strValue) {
                                var N = M.haystackPts;
                                if (r = N[0].x, i = N[0].y, n = N[1].x, a = N[1].y, r > n) {
                                    var B = r;
                                    r = n, n = B
                                }
                                if (i > a) {
                                    var B = i;
                                    i = a, a = B
                                }
                                d(g, r - P, i - P, n + P, a + P)
                            } else {
                                for (var z = M.bezierPts || M.linePts || [], I = 0; I < z.length; I++) {
                                    var L = z[I];
                                    r = L.x - P, n = L.x + P, i = L.y - P, a = L.y + P, d(g, r, i, n, a)
                                }
                                if (0 === z.length) {
                                    var O = y.source,
                                        A = O._private,
                                        R = A.position,
                                        q = y.target,
                                        V = q._private,
                                        F = V.position;
                                    if (r = R.x, n = F.x, i = R.y, a = F.y, r > n) {
                                        var B = r;
                                        r = n, n = B
                                    }
                                    if (i > a) {
                                        var B = i;
                                        i = a, a = B
                                    }
                                    r -= P, n += P, i -= P, a += P, d(g, r, i, n, a)
                                }
                            }
                        }
                        if (p) {
                            if (r = g.x1, n = g.x2, i = g.y1, a = g.y2, t.includeShadows && e.pstyle("shadow-opacity").value > 0) {
                                var j = e.pstyle("shadow-blur").pfValue / 2,
                                    X = e.pstyle("shadow-offset-x").pfValue,
                                    Y = e.pstyle("shadow-offset-y").pfValue;
                                d(g, r - j + X, i - j + Y, n + j + X, a + j + Y)
                            }
                            d(g, r - _, i - _, n + _, a + _)
                        }
                        p && t.includeEdges && x && (f(g, e, "mid-source", t), f(g, e, "mid-target", t), f(g, e, "source", t), f(g, e, "target", t)), p && t.includeLabels && (v(g, e, null, t), x && (v(g, e, "source", t), v(g, e, "target", t)))
                    }
                    return g.x1 = c(g.x1), g.y1 = c(g.y1), g.x2 = c(g.x2), g.y2 = c(g.y2), g.w = c(g.x2 - g.x1), g.h = c(g.y2 - g.y1), g.w > 0 && g.h > 0 && w && u.expandBoundingBox(g, 1), g
                },
                y = function (e) {
                    return e ? "t" : "f"
                },
                m = function (e) {
                    var t = "";
                    return t += y(e.incudeNodes), t += y(e.includeEdges), t += y(e.includeLabels), t += y(e.includeShadows), t += y(e.includeOverlays)
                },
                b = function (e, t) {
                    var r, n = e._private,
                        i = e.cy().headless(),
                        a = t === x ? w : m(t);
                    return t.useCache && !i && n.bbCache && n.bbCache[a] ? r = n.bbCache[a] : (r = g(e, t), i || (n.bbCache = n.bbCache || {}, n.bbCache[a] = r)), r
                },
                x = {
                    includeNodes: !0,
                    includeEdges: !0,
                    includeLabels: !0,
                    includeShadows: !0,
                    includeOverlays: !0,
                    useCache: !0
                },
                w = m(x);
            a.recalculateRenderedStyle = function (e) {
                var t = this.cy(),
                    r = t.renderer(),
                    n = t.styleEnabled();
                return r && n && r.recalculateRenderedStyle(this, e), this
            }, a.boundingBox = function (e) {
                if (1 === this.length && this[0]._private.bbCache && (void 0 === e || void 0 === e.useCache || e.useCache === !0)) return e = void 0 === e ? x : n(e), b(this[0], e);
                var t = {
                    x1: 1 / 0,
                    y1: 1 / 0,
                    x2: -(1 / 0),
                    y2: -(1 / 0)
                };
                e = e || l.staticEmptyObject();
                var r = n(e),
                    i = this,
                    a = i.cy(),
                    o = a.styleEnabled();
                o && this.recalculateRenderedStyle(r.useCache);
                for (var s = 0; s < i.length; s++) {
                    var u = i[s];
                    o && u.isEdge() && "bezier" === u.pstyle("curve-style").strValue && u.parallelEdges().recalculateRenderedStyle(r.useCache), h(t, b(u, r))
                }
                return t.x1 = c(t.x1), t.y1 = c(t.y1), t.x2 = c(t.x2), t.y2 = c(t.y2), t.w = c(t.x2 - t.x1), t.h = c(t.y2 - t.y1), t
            };
            var E = function (e) {
                e.uppercaseName = l.capitalize(e.name), e.autoName = "auto" + e.uppercaseName, e.labelName = "label" + e.uppercaseName, e.outerName = "outer" + e.uppercaseName, e.uppercaseOuterName = l.capitalize(e.outerName), i[e.name] = function () {
                    var t = this[0],
                        r = t._private,
                        n = r.cy,
                        i = n._private.styleEnabled;
                    if (t) {
                        if (!i) return 1;
                        if (t.isParent()) return r[e.autoName] || 0;
                        var a = t.pstyle(e.name);
                        switch (a.strValue) {
                            case "label":
                                return r.rstyle[e.labelName] || 0;
                            default:
                                return a.pfValue
                        }
                    }
                }, i["outer" + e.uppercaseName] = function () {
                    var t = this[0],
                        r = t._private,
                        n = r.cy,
                        i = n._private.styleEnabled;
                    if (t) {
                        if (i) {
                            var a = t[e.name](),
                                o = t.pstyle("border-width").pfValue,
                                s = 2 * t.pstyle("padding").pfValue;
                            return a + o + s
                        }
                        return 1
                    }
                }, i["rendered" + e.uppercaseName] = function () {
                    var t = this[0];
                    if (t) {
                        var r = t[e.name]();
                        return r * this.cy().zoom()
                    }
                }, i["rendered" + e.uppercaseOuterName] = function () {
                    var t = this[0];
                    if (t) {
                        var r = t[e.outerName]();
                        return r * this.cy().zoom()
                    }
                }
            };
            E({
                name: "width"
            }), E({
                name: "height"
            }), i.modelPosition = i.point = i.position, i.modelPositions = i.points = i.positions, i.renderedPoint = i.renderedPosition, i.relativePoint = i.relativePosition, i.boundingbox = i.boundingBox, i.renderedBoundingbox = i.renderedBoundingBox, t.exports = a
        }, {
            "../define": 44,
            "../is": 83,
            "../math": 85,
            "../util": 100
        }],
        22: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = function (e, t, r) {
                    var a = this;
                    if (r = !(void 0 !== r && !r), void 0 === e || void 0 === t || !i.core(e)) return void n.error("An element must have a core reference and parameters set");
                    var o = t.group;
                    if (null == o && (o = t.data && null != t.data.source && null != t.data.target ? "edges" : "nodes"), "nodes" !== o && "edges" !== o) return void n.error("An element must be of type `nodes` or `edges`; you specified `" + o + "`");
                    if (this.length = 1, this[0] = this, this._private = {
                        cy: e,
                        single: !0,
                        data: t.data || {},
                        position: t.position || {},
                        autoWidth: void 0,
                        autoHeight: void 0,
                        listeners: [],
                        group: o,
                        style: {},
                        rstyle: {},
                        styleCxts: [],
                        removed: !0,
                        selected: !!t.selected,
                        selectable: void 0 === t.selectable ? !0 : !!t.selectable,
                        locked: !!t.locked,
                        grabbed: !1,
                        grabbable: void 0 === t.grabbable ? !0 : !!t.grabbable,
                        active: !1,
                        classes: {},
                        animation: {
                            current: [],
                            queue: []
                        },
                        rscratch: {},
                        scratch: t.scratch || {},
                        edges: [],
                        children: [],
                        traversalCache: {}
                    }, t.renderedPosition) {
                        var s = t.renderedPosition,
                            l = e.pan(),
                            u = e.zoom();
                        this._private.position = {
                            x: (s.x - l.x) / u,
                            y: (s.y - l.y) / u
                        }
                    }
                    if (i.string(t.classes))
                        for (var c = t.classes.split(/\s+/), d = 0, h = c.length; h > d; d++) {
                            var p = c[d];
                            p && "" !== p && (a._private.classes[p] = !0)
                        } (t.style || t.css) && e.style().applyBypass(this, t.style || t.css), (void 0 === r || r) && this.restore()
                };
            t.exports = a
        }, {
            "../is": 83,
            "../util": 100
        }],
        23: [function (e, t, r) {
            "use strict";
            var n = e("../define"),
                i = {
                    on: n.on(),
                    one: n.on({
                        unbindSelfOnTrigger: !0
                    }),
                    once: n.on({
                        unbindAllBindersOnTrigger: !0
                    }),
                    off: n.off(),
                    trigger: n.trigger(),
                    rtrigger: function (e, t) {
                        return 0 !== this.length ? (this.cy().notify({
                            type: e,
                            eles: this
                        }), this.trigger(e, t), this) : void 0
                    }
                };
            n.eventAliasesOn(i), t.exports = i
        }, {
            "../define": 44
        }],
        24: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../selector"),
                a = {
                    nodes: function (e) {
                        return this.filter(function (e, t) {
                            return t.isNode()
                        }).filter(e)
                    },
                    edges: function (e) {
                        return this.filter(function (e, t) {
                            return t.isEdge()
                        }).filter(e)
                    },
                    filter: function (e) {
                        if (void 0 === e) return this;
                        if (n.string(e) || n.elementOrCollection(e)) return i(e).filter(this);
                        if (n.fn(e)) {
                            for (var t = [], r = 0; r < this.length; r++) {
                                var a = this[r];
                                e.apply(a, [r, a]) && t.push(a)
                            }
                            return this.spawn(t)
                        }
                        return this.spawn()
                    },
                    not: function (e) {
                        if (e) {
                            n.string(e) && (e = this.filter(e));
                            for (var t = [], r = 0; r < this.length; r++) {
                                var i = this[r],
                                    a = e._private.ids[i.id()];
                                a || t.push(i)
                            }
                            return this.spawn(t)
                        }
                        return this
                    },
                    absoluteComplement: function () {
                        var e = this._private.cy;
                        return e.mutableElements().not(this)
                    },
                    intersect: function (e) {
                        if (n.string(e)) {
                            var t = e;
                            return this.filter(t)
                        }
                        for (var r = [], i = this, a = e, o = this.length < e.length, s = o ? a._private.ids : i._private.ids, l = o ? i : a, u = 0; u < l.length; u++) {
                            var c = l[u]._private.data.id,
                                d = s[c];
                            d && r.push(d)
                        }
                        return this.spawn(r)
                    },
                    xor: function (e) {
                        var t = this._private.cy;
                        n.string(e) && (e = t.$(e));
                        var r = [],
                            i = this,
                            a = e,
                            o = function (e, t) {
                                for (var n = 0; n < e.length; n++) {
                                    var i = e[n],
                                        a = i._private.data.id,
                                        o = t._private.ids[a];
                                    o || r.push(i)
                                }
                            };
                        return o(i, a), o(a, i), this.spawn(r)
                    },
                    diff: function (e) {
                        var t = this._private.cy;
                        n.string(e) && (e = t.$(e));
                        var r = [],
                            i = [],
                            a = [],
                            o = this,
                            s = e,
                            l = function (e, t, r) {
                                for (var n = 0; n < e.length; n++) {
                                    var i = e[n],
                                        o = i._private.data.id,
                                        s = t._private.ids[o];
                                    s ? a.push(i) : r.push(i)
                                }
                            };
                        return l(o, s, r), l(s, o, i), {
                            left: this.spawn(r, {
                                unique: !0
                            }),
                            right: this.spawn(i, {
                                unique: !0
                            }),
                            both: this.spawn(a, {
                                unique: !0
                            })
                        }
                    },
                    add: function (e) {
                        var t = this._private.cy;
                        if (!e) return this;
                        if (n.string(e)) {
                            var r = e;
                            e = t.mutableElements().filter(r)
                        }
                        for (var i = [], a = 0; a < this.length; a++) i.push(this[a]);
                        for (var a = 0; a < e.length; a++) {
                            var o = !this._private.ids[e[a].id()];
                            o && i.push(e[a])
                        }
                        return this.spawn(i)
                    },
                    merge: function (e) {
                        var t = this._private,
                            r = t.cy;
                        if (!e) return this;
                        if (e && n.string(e)) {
                            var i = e;
                            e = r.mutableElements().filter(i)
                        }
                        for (var a = 0; a < e.length; a++) {
                            var o = e[a],
                                s = o._private.data.id,
                                l = !t.ids[s];
                            if (l) {
                                var u = this.length++;
                                this[u] = o, t.ids[s] = o, t.indexes[s] = u
                            } else {
                                var u = t.indexes[s];
                                this[u] = o, t.ids[s] = o
                            }
                        }
                        return this
                    },
                    unmergeOne: function (e) {
                        e = e[0];
                        var t = this._private,
                            r = e._private.data.id,
                            n = t.indexes[r];
                        if (null == n) return this;
                        this[n] = void 0, t.ids[r] = void 0, t.indexes[r] = void 0;
                        var i = n === this.length - 1;
                        if (this.length > 1 && !i) {
                            var a = this.length - 1,
                                o = this[a],
                                s = o._private.data.id;
                            this[a] = void 0, this[n] = o, t.indexes[s] = n
                        }
                        return this.length-- , this
                    },
                    unmerge: function (e) {
                        var t = this._private.cy;
                        if (!e) return this;
                        if (e && n.string(e)) {
                            var r = e;
                            e = t.mutableElements().filter(r)
                        }
                        for (var i = 0; i < e.length; i++) this.unmergeOne(e[i]);
                        return this
                    },
                    map: function (e, t) {
                        for (var r = [], n = this, i = 0; i < n.length; i++) {
                            var a = n[i],
                                o = t ? e.apply(t, [a, i, n]) : e(a, i, n);
                            r.push(o)
                        }
                        return r
                    },
                    stdFilter: function (e, t) {
                        for (var r = [], n = this, i = 0; i < n.length; i++) {
                            var a = n[i],
                                o = t ? e.apply(t, [a, i, n]) : e(a, i, n);
                            o && r.push(a)
                        }
                        return this.spawn(r)
                    },
                    max: function (e, t) {
                        for (var r, n = -(1 / 0), i = this, a = 0; a < i.length; a++) {
                            var o = i[a],
                                s = t ? e.apply(t, [o, a, i]) : e(o, a, i);
                            s > n && (n = s, r = o)
                        }
                        return {
                            value: n,
                            ele: r
                        }
                    },
                    min: function (e, t) {
                        for (var r, n = 1 / 0, i = this, a = 0; a < i.length; a++) {
                            var o = i[a],
                                s = t ? e.apply(t, [o, a, i]) : e(o, a, i);
                            n > s && (n = s, r = o)
                        }
                        return {
                            value: n,
                            ele: r
                        }
                    }
                },
                o = a;
            o.u = o["|"] = o["+"] = o.union = o.or = o.add, o["\\"] = o["!"] = o["-"] = o.difference = o.relativeComplement = o.subtract = o.not, o.n = o["&"] = o["."] = o.and = o.intersection = o.intersect, o["^"] = o["(+)"] = o["(-)"] = o.symmetricDifference = o.symdiff = o.xor, o.fnFilter = o.filterFn = o.stdFilter, o.complement = o.abscomp = o.absoluteComplement, t.exports = a
        }, {
            "../is": 83,
            "../selector": 87
        }],
        25: [function (e, t, r) {
            "use strict";
            var n = {
                isNode: function () {
                    return "nodes" === this.group()
                },
                isEdge: function () {
                    return "edges" === this.group()
                },
                isLoop: function () {
                    return this.isEdge() && this.source().id() === this.target().id()
                },
                isSimple: function () {
                    return this.isEdge() && this.source().id() !== this.target().id()
                },
                group: function () {
                    var e = this[0];
                    return e ? e._private.group : void 0
                }
            };
            t.exports = n
        }, {}],
        26: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = e("./element"),
                o = {
                    generate: function (e, t, r) {
                        for (var i = null != r ? r : n.uuid(); e.hasElementWithId(i);) i = n.uuid();
                        return i
                    }
                },
                s = function (e, t, r) {
                    if (void 0 === e || !i.core(e)) return void n.error("A collection must have a reference to the core");
                    var s = {},
                        l = {},
                        u = !1;
                    if (t) {
                        if (t.length > 0 && i.plainObject(t[0]) && !i.element(t[0])) {
                            u = !0;
                            for (var c = [], d = {}, h = 0, p = t.length; p > h; h++) {
                                var f = t[h];
                                null == f.data && (f.data = {});
                                var v = f.data;
                                if (null == v.id) v.id = o.generate(e, f);
                                else if (e.hasElementWithId(v.id) || d[v.id]) continue;
                                var g = new a(e, f, !1);
                                c.push(g), d[v.id] = !0
                            }
                            t = c
                        }
                    } else t = [];
                    this.length = 0;
                    for (var h = 0, p = t.length; p > h; h++) {
                        var y = t[h];
                        if (y) {
                            var m = y._private.data.id;
                            (!r || r.unique && !s[m]) && (s[m] = y, l[m] = this.length, this[this.length] = y, this.length++)
                        }
                    }
                    this._private = {
                        cy: e,
                        ids: s,
                        indexes: l
                    }, u && this.restore()
                },
                l = a.prototype = s.prototype;
            l.instanceString = function () {
                return "collection"
            }, l.spawn = function (e, t, r) {
                return i.core(e) || (r = t, t = e, e = this.cy()), new s(e, t, r)
            }, l.spawnSelf = function () {
                return this.spawn(this)
            }, l.cy = function () {
                return this._private.cy
            }, l.element = function () {
                return this[0]
            }, l.collection = function () {
                return i.collection(this) ? this : new s(this._private.cy, [this])
            }, l.unique = function () {
                return new s(this._private.cy, this, {
                    unique: !0
                })
            }, l.hasElementWithId = function (e) {
                return !!this._private.ids[e]
            }, l.getElementById = function (e) {
                var t = this._private.cy,
                    r = this._private.ids[e];
                return r ? r : new s(t)
            }, l.poolIndex = function () {
                var e = this._private.cy,
                    t = e._private.elements,
                    r = this._private.data.id;
                return t._private.indexes[r]
            }, l.json = function (e) {
                var t = this.element(),
                    r = this.cy();
                if (null == t && e) return this;
                if (null != t) {
                    var a = t._private;
                    if (i.plainObject(e)) {
                        r.startBatch(), e.data && t.data(e.data), e.position && t.position(e.position);
                        var o = function (r, n, i) {
                            var o = e[r];
                            null != o && o !== a[r] && (o ? t[n]() : t[i]())
                        };
                        return o("removed", "remove", "restore"), o("selected", "select", "unselect"), o("selectable", "selectify", "unselectify"), o("locked", "lock", "unlock"), o("grabbable", "grabify", "ungrabify"), null != e.classes && t.classes(e.classes), r.endBatch(), this
                    }
                    if (void 0 === e) {
                        var s = {
                            data: n.copy(a.data),
                            position: n.copy(a.position),
                            group: a.group,
                            removed: a.removed,
                            selected: a.selected,
                            selectable: a.selectable,
                            locked: a.locked,
                            grabbable: a.grabbable,
                            classes: null
                        };
                        return s.classes = Object.keys(a.classes).filter(function (e) {
                            return a.classes[e]
                        }).join(" "), s
                    }
                }
            }, l.jsons = function () {
                for (var e = [], t = 0; t < this.length; t++) {
                    var r = this[t],
                        n = r.json();
                    e.push(n)
                }
                return e
            }, l.clone = function () {
                for (var e = this.cy(), t = [], r = 0; r < this.length; r++) {
                    var n = this[r],
                        i = n.json(),
                        o = new a(e, i, !1);
                    t.push(o)
                }
                return new s(e, t)
            }, l.copy = l.clone, l.restore = function (e) {
                var t = this,
                    r = t.cy(),
                    a = r._private;
                void 0 === e && (e = !0);
                for (var l, u = [], c = [], d = 0, h = t.length; h > d; d++) {
                    var p = t[d];
                    p.removed() && (p.isNode() ? u.push(p) : c.push(p))
                }
                l = u.concat(c);
                var d, f = function () {
                    l.splice(d, 1), d--
                };
                for (d = 0; d < l.length; d++) {
                    var p = l[d],
                        v = p._private,
                        g = v.data;
                    if (v.traversalCache = null, void 0 === g.id) g.id = o.generate(r, p);
                    else if (i.number(g.id)) g.id = "" + g.id;
                    else {
                        if (i.emptyString(g.id) || !i.string(g.id)) {
                            n.error("Can not create element with invalid string ID `" + g.id + "`"), f();
                            continue
                        }
                        if (r.hasElementWithId(g.id)) {
                            n.error("Can not create second element with ID `" + g.id + "`"), f();
                            continue
                        }
                    }
                    var y = g.id;
                    if (p.isNode()) {
                        var m = p,
                            b = v.position;
                        null == b.x && (b.x = 0), null == b.y && (b.y = 0)
                    }
                    if (p.isEdge()) {
                        for (var x = p, w = ["source", "target"], E = w.length, _ = !1, S = 0; E > S; S++) {
                            var P = w[S],
                                T = g[P];
                            i.number(T) && (T = g[P] = "" + g[P]), null == T || "" === T ? (n.error("Can not create edge `" + y + "` with unspecified " + P), _ = !0) : r.hasElementWithId(T) || (n.error("Can not create edge `" + y + "` with nonexistant " + P + " `" + T + "`"), _ = !0)
                        }
                        if (_) {
                            f();
                            continue
                        }
                        var k = r.getElementById(g.source),
                            D = r.getElementById(g.target);
                        k._private.edges.push(x), D._private.edges.push(x), x._private.source = k, x._private.target = D
                    }
                    v.ids = {}, v.ids[y] = p, v.indexes = {}, v.indexes[y] = p, v.removed = !1, r.addToPool(p)
                }
                for (var d = 0; d < u.length; d++) {
                    var m = u[d],
                        g = m._private.data;
                    i.number(g.parent) && (g.parent = "" + g.parent);
                    var C = g.parent,
                        M = null != C;
                    if (M) {
                        var N = r.getElementById(C);
                        if (N.empty()) g.parent = void 0;
                        else {
                            for (var B = !1, z = N; !z.empty();) {
                                if (m.same(z)) {
                                    B = !0, g.parent = void 0;
                                    break
                                }
                                z = z.parent()
                            }
                            B || (N[0]._private.children.push(m), m._private.parent = N[0], a.hasCompoundNodes = !0)
                        }
                    }
                }
                if (l.length > 0) {
                    for (var I = new s(r, l), d = 0; d < I.length; d++) {
                        var p = I[d];
                        if (!p.isNode()) {
                            for (var L = p.parallelEdges(), S = 0; S < L.length; S++) L[S]._private.traversalCache = null;
                            p.source()[0]._private.traversalCache = null, p.target()[0]._private.traversalCache = null
                        }
                    }
                    var O;
                    O = a.hasCompoundNodes ? I.add(I.connectedNodes()).add(I.parent()) : I, O.updateStyle(e), e ? I.rtrigger("add") : I.trigger("add")
                }
                return t
            }, l.removed = function () {
                var e = this[0];
                return e && e._private.removed
            }, l.inside = function () {
                var e = this[0];
                return e && !e._private.removed
            }, l.remove = function (e) {
                function t(e) {
                    for (var t = e._private.edges, r = 0; r < t.length; r++) i(t[r])
                }

                function r(e) {
                    for (var t = e._private.children, r = 0; r < t.length; r++) i(t[r])
                }

                function i(e) {
                    var n = h[e.id()];
                    n || (h[e.id()] = !0, e.isNode() ? (d.push(e), t(e), r(e)) : d.unshift(e))
                }

                function a(e, t) {
                    var r = e._private.edges;
                    n.removeFromArray(r, t), e._private.traversalCache = null
                }

                function o(e) {
                    for (var t = e.parallelEdges(), r = 0; r < t.length; r++) t[r]._private.traversalCache = null
                }

                function l(e, t) {
                    t = t[0], e = e[0];
                    var r = e._private.children,
                        i = e.id();
                    n.removeFromArray(r, t), y.ids[i] || (y.ids[i] = !0, y.push(e))
                }
                var u = this,
                    c = [],
                    d = [],
                    h = {},
                    p = u._private.cy;
                void 0 === e && (e = !0);
                for (var f = 0, v = u.length; v > f; f++) {
                    var g = u[f];
                    i(g)
                }
                var y = [];
                y.ids = {}, p.removeFromPool(d);
                for (var f = 0; f < d.length; f++) {
                    var g = d[f];
                    if (g._private.removed = !0, c.push(g), g.isEdge()) {
                        var m = g.source()[0],
                            b = g.target()[0];
                        a(m, g), a(b, g), o(g)
                    } else {
                        var x = g.parent();
                        0 !== x.length && l(x, g)
                    }
                }
                var w = p._private.elements;
                p._private.hasCompoundNodes = !1;
                for (var f = 0; f < w.length; f++) {
                    var g = w[f];
                    if (g.isParent()) {
                        p._private.hasCompoundNodes = !0;
                        break
                    }
                }
                var E = new s(this.cy(), c);
                E.size() > 0 && (e && this.cy().notify({
                    type: "remove",
                    eles: E
                }), E.trigger("remove"));
                for (var f = 0; f < y.length; f++) {
                    var g = y[f];
                    g.removed() || g.updateStyle()
                }
                return new s(p, c)
            }, l.move = function (e) {
                var t = this._private.cy;
                if (void 0 !== e.source || void 0 !== e.target) {
                    var r = e.source,
                        n = e.target,
                        i = t.hasElementWithId(r),
                        a = t.hasElementWithId(n);
                    if (i || a) {
                        var o = this.jsons();
                        this.remove();
                        for (var s = 0; s < o.length; s++) {
                            var l = o[s],
                                u = this[s];
                            "edges" === l.group && (i && (l.data.source = r), a && (l.data.target = n), l.scratch = u._private.scratch)
                        }
                        return t.add(o)
                    }
                } else if (void 0 !== e.parent) {
                    var c = e.parent,
                        d = null === c || t.hasElementWithId(c);
                    if (d) {
                        var o = this.jsons(),
                            h = this.descendants(),
                            p = h.union(h.union(this).connectedEdges()).jsons();
                        this.remove();
                        for (var s = 0; s < o.length; s++) {
                            var l = o[s],
                                u = this[s];
                            "nodes" === l.group && (l.data.parent = null === c ? void 0 : c, l.scratch = u._private.scratch)
                        }
                        return t.add(o.concat(p))
                    }
                }
                return this
            }, [e("./algorithms"), e("./animation"), e("./class"), e("./comparators"), e("./compounds"), e("./data"), e("./degree"), e("./dimensions"), e("./events"), e("./filter"), e("./group"), e("./index"), e("./iteration"), e("./layout"), e("./style"), e("./switch-functions"), e("./traversing")].forEach(function (e) {
                n.extend(l, e)
            }), t.exports = s
        }, {
            "../is": 83,
            "../util": 100,
            "./algorithms": 11,
            "./animation": 15,
            "./class": 16,
            "./comparators": 17,
            "./compounds": 18,
            "./data": 19,
            "./degree": 20,
            "./dimensions": 21,
            "./element": 22,
            "./events": 23,
            "./filter": 24,
            "./group": 25,
            "./index": 26,
            "./iteration": 27,
            "./layout": 28,
            "./style": 29,
            "./switch-functions": 30,
            "./traversing": 31
        }],
        27: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("./zsort"),
                a = {
                    each: function (e) {
                        if (n.fn(e))
                            for (var t = 0; t < this.length; t++) {
                                var r = this[t],
                                    i = e.apply(r, [t, r]);
                                if (i === !1) break
                            }
                        return this
                    },
                    forEach: function (e, t) {
                        if (n.fn(e))
                            for (var r = 0; r < this.length; r++) {
                                var i = this[r],
                                    a = t ? e.apply(t, [i, r, this]) : e(i, r, this);
                                if (a === !1) break
                            }
                        return this
                    },
                    toArray: function () {
                        for (var e = [], t = 0; t < this.length; t++) e.push(this[t]);
                        return e
                    },
                    slice: function (e, t) {
                        var r = [],
                            n = this.length;
                        null == t && (t = n), null == e && (e = 0), 0 > e && (e = n + e), 0 > t && (t = n + t);
                        for (var i = e; i >= 0 && t > i && n > i; i++) r.push(this[i]);
                        return this.spawn(r)
                    },
                    size: function () {
                        return this.length
                    },
                    eq: function (e) {
                        return this[e] || this.spawn()
                    },
                    first: function () {
                        return this[0] || this.spawn()
                    },
                    last: function () {
                        return this[this.length - 1] || this.spawn()
                    },
                    empty: function () {
                        return 0 === this.length
                    },
                    nonempty: function () {
                        return !this.empty()
                    },
                    sort: function (e) {
                        if (!n.fn(e)) return this;
                        var t = this.toArray().sort(e);
                        return this.spawn(t)
                    },
                    sortByZIndex: function () {
                        return this.sort(i)
                    },
                    zDepth: function () {
                        var e = this[0];
                        if (e) {
                            var t = e._private,
                                r = t.group;
                            if ("nodes" === r) {
                                var n = t.data.parent ? e.parents().size() : 0;
                                return e.isParent() ? n : Number.MAX_VALUE
                            }
                            var i = t.source,
                                a = t.target,
                                o = i.zDepth(),
                                s = a.zDepth();
                            return Math.max(o, s, 0)
                        }
                    }
                };
            t.exports = a
        }, {
            "../is": 83,
            "./zsort": 32
        }],
        28: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../util"),
                a = e("../promise"),
                o = {
                    layoutPositions: function (e, t, r) {
                        var i = this.nodes(),
                            o = this.cy();
                        if (e.trigger({
                            type: "layoutstart",
                            layout: e
                        }), e.animations = [], t.animate) {
                            for (var s = 0; s < i.length; s++) {
                                var l = i[s],
                                    u = r.call(l, s, l),
                                    c = l.position();
                                n.number(c.x) && n.number(c.y) || l.silentPosition({
                                    x: 0,
                                    y: 0
                                });
                                var d = l.animation({
                                    position: u,
                                    duration: t.animationDuration,
                                    easing: t.animationEasing
                                });
                                e.animations.push(d), d.play()
                            }
                            var h;
                            o.on("step.*", h = function () {
                                t.fit && o.fit(t.eles, t.padding)
                            }), e.one("layoutstop", function () {
                                o.off("step.*", h)
                            }), e.one("layoutready", t.ready), e.trigger({
                                type: "layoutready",
                                layout: e
                            }), a.all(e.animations.map(function (e) {
                                return e.promise()
                            })).then(function () {
                                o.off("step.*", h), null != t.zoom && o.zoom(t.zoom), t.pan && o.pan(t.pan), t.fit && o.fit(t.eles, t.padding), e.one("layoutstop", t.stop), e.trigger({
                                    type: "layoutstop",
                                    layout: e
                                })
                            })
                        } else i.positions(r), t.fit && o.fit(t.eles, t.padding), null != t.zoom && o.zoom(t.zoom), t.pan && o.pan(t.pan), e.one("layoutready", t.ready), e.trigger({
                            type: "layoutready",
                            layout: e
                        }), e.one("layoutstop", t.stop), e.trigger({
                            type: "layoutstop",
                            layout: e
                        });
                        return this
                    },
                    layout: function (e) {
                        var t = this.cy();
                        return t.layout(i.extend({}, e, {
                            eles: this
                        })), this
                    },
                    makeLayout: function (e) {
                        var t = this.cy();
                        return t.makeLayout(i.extend({}, e, {
                            eles: this
                        }))
                    }
                };
            o.createLayout = o.makeLayout, t.exports = o
        }, {
            "../is": 83,
            "../promise": 86,
            "../util": 100
        }],
        29: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = {
                    updateStyle: function (e) {
                        var t = this._private.cy;
                        if (!t.styleEnabled()) return this;
                        if (t._private.batchingStyle) {
                            var r = t._private.batchStyleEles;
                            return r.merge(this), this
                        }
                        var n = t.hasCompoundNodes(),
                            i = t.style(),
                            a = this;
                        if (e = !(!e && void 0 !== e), n && (a = this.spawnSelf().merge(this.descendants()).merge(this.parents())), i.apply(a), n) {
                            a.updateCompoundBounds()
                        }
                        return e ? a.rtrigger("style") : a.trigger("style"), this
                    },
                    updateMappers: function (e) {
                        var t = this._private.cy,
                            r = t.style();
                        if (e = !(!e && void 0 !== e), !t.styleEnabled()) return this;
                        r.updateMappers(this);
                        var n = this.updateCompoundBounds(),
                            i = n.length > 0 ? this.add(n) : this;
                        return e ? i.rtrigger("style") : i.trigger("style"), this
                    },
                    parsedStyle: function (e) {
                        var t = this[0];
                        if (t.cy().styleEnabled()) return t ? t._private.style[e] || t.cy().style().getDefaultProperty(e) : void 0
                    },
                    renderedStyle: function (e) {
                        var t = this.cy();
                        if (!t.styleEnabled()) return this;
                        var r = this[0];
                        if (r) {
                            var n = r.cy().style().getRenderedStyle(r);
                            return void 0 === e ? n : n[e]
                        }
                    },
                    style: function (e, t) {
                        var r = this.cy();
                        if (!r.styleEnabled()) return this;
                        var i = !1,
                            a = r.style();
                        if (n.plainObject(e)) {
                            var o = e;
                            a.applyBypass(this, o, i);
                            var s = this.updateCompoundBounds(),
                                l = s.length > 0 ? this.add(s) : this;
                            l.rtrigger("style")
                        } else if (n.string(e)) {
                            if (void 0 === t) {
                                var u = this[0];
                                return u ? a.getStylePropertyValue(u, e) : void 0
                            }
                            a.applyBypass(this, e, t, i);
                            var s = this.updateCompoundBounds(),
                                l = s.length > 0 ? this.add(s) : this;
                            l.rtrigger("style")
                        } else if (void 0 === e) {
                            var u = this[0];
                            return u ? a.getRawStyle(u) : void 0
                        }
                        return this
                    },
                    removeStyle: function (e) {
                        var t = this.cy();
                        if (!t.styleEnabled()) return this;
                        var r = !1,
                            n = t.style(),
                            i = this;
                        if (void 0 === e)
                            for (var a = 0; a < i.length; a++) {
                                var o = i[a];
                                n.removeAllBypasses(o, r)
                            } else {
                            e = e.split(/\s+/);
                            for (var a = 0; a < i.length; a++) {
                                var o = i[a];
                                n.removeBypasses(o, e, r)
                            }
                        }
                        var s = this.updateCompoundBounds(),
                            l = s.length > 0 ? this.add(s) : this;
                        return l.rtrigger("style"), this
                    },
                    show: function () {
                        return this.css("display", "element"), this
                    },
                    hide: function () {
                        return this.css("display", "none"), this
                    },
                    visible: function () {
                        var e = this.cy();
                        if (!e.styleEnabled()) return !0;
                        var t = this[0],
                            r = e.hasCompoundNodes();
                        if (t) {
                            if ("visible" !== t.pstyle("visibility").value || "element" !== t.pstyle("display").value || 0 === t.pstyle("width").pfValue) return !1;
                            if ("nodes" === t._private.group) {
                                if (0 === t.pstyle("height").pfValue) return !1;
                                if (!r) return !0;
                                var n = t._private.data.parent ? t.parents() : null;
                                if (n)
                                    for (var i = 0; i < n.length; i++) {
                                        var a = n[i],
                                            o = a.pstyle("visibility").value,
                                            s = a.pstyle("display").value;
                                        if ("visible" !== o || "element" !== s) return !1
                                    }
                                return !0
                            }
                            var l = t._private.source,
                                u = t._private.target;
                            return l.visible() && u.visible()
                        }
                    },
                    hidden: function () {
                        var e = this[0];
                        return e ? !e.visible() : void 0
                    },
                    effectiveOpacity: function () {
                        var e = this.cy();
                        if (!e.styleEnabled()) return 1;
                        var t = e.hasCompoundNodes(),
                            r = this[0];
                        if (r) {
                            var n = r._private,
                                i = r.pstyle("opacity").value;
                            if (!t) return i;
                            var a = n.data.parent ? r.parents() : null;
                            if (a)
                                for (var o = 0; o < a.length; o++) {
                                    var s = a[o],
                                        l = s.pstyle("opacity").value;
                                    i = l * i
                                }
                            return i
                        }
                    },
                    transparent: function () {
                        var e = this.cy();
                        if (!e.styleEnabled()) return !1;
                        var t = this[0],
                            r = t.cy().hasCompoundNodes();
                        return t ? r ? 0 === t.effectiveOpacity() : 0 === t.pstyle("opacity").value : void 0
                    },
                    backgrounding: function () {
                        var e = this.cy();
                        if (!e.styleEnabled()) return !1;
                        var t = this[0];
                        return !!t._private.backgrounding
                    }
                };
            i.bypass = i.css = i.style, i.renderedCss = i.renderedStyle, i.removeBypass = i.removeCss = i.removeStyle, i.pstyle = i.parsedStyle, t.exports = i
        }, {
            "../is": 83
        }],
        30: [function (e, t, r) {
            "use strict";

            function n(e) {
                return function () {
                    var t = arguments,
                        r = [];
                    if (2 === t.length) {
                        var n = t[0],
                            i = t[1];
                        this.on(e.event, n, i)
                    } else if (1 === t.length) {
                        var i = t[0];
                        this.on(e.event, i)
                    } else if (0 === t.length) {
                        for (var a = 0; a < this.length; a++) {
                            var o = this[a],
                                s = !e.ableField || o._private[e.ableField],
                                l = o._private[e.field] != e.value;
                            if (e.overrideAble) {
                                var u = e.overrideAble(o);
                                if (void 0 !== u && (s = u, !u)) return this
                            }
                            s && (o._private[e.field] = e.value, l && r.push(o))
                        }
                        var c = this.spawn(r);
                        c.updateStyle(), c.trigger(e.event)
                    }
                    return this
                }
            }

            function i(e) {
                a[e.field] = function () {
                    var t = this[0];
                    if (t) {
                        if (e.overrideField) {
                            var r = e.overrideField(t);
                            if (void 0 !== r) return r
                        }
                        return t._private[e.field]
                    }
                }, a[e.on] = n({
                    event: e.on,
                    field: e.field,
                    ableField: e.ableField,
                    overrideAble: e.overrideAble,
                    value: !0
                }), a[e.off] = n({
                    event: e.off,
                    field: e.field,
                    ableField: e.ableField,
                    overrideAble: e.overrideAble,
                    value: !1
                })
            }
            var a = {};
            i({
                field: "locked",
                overrideField: function (e) {
                    return e.cy().autolock() ? !0 : void 0
                },
                on: "lock",
                off: "unlock"
            }), i({
                field: "grabbable",
                overrideField: function (e) {
                    return e.cy().autoungrabify() ? !1 : void 0
                },
                on: "grabify",
                off: "ungrabify"
            }), i({
                field: "selected",
                ableField: "selectable",
                overrideAble: function (e) {
                    return e.cy().autounselectify() ? !1 : void 0
                },
                on: "select",
                off: "unselect"
            }), i({
                field: "selectable",
                overrideField: function (e) {
                    return e.cy().autounselectify() ? !1 : void 0
                },
                on: "selectify",
                off: "unselectify"
            }), a.deselect = a.unselect, a.grabbed = function () {
                var e = this[0];
                return e ? e._private.grabbed : void 0
            }, i({
                field: "active",
                on: "activate",
                off: "unactivate"
            }), a.inactive = function () {
                var e = this[0];
                return e ? !e._private.active : void 0
            }, t.exports = a
        }, {}],
        31: [function (e, t, r) {
            "use strict";

            function n(e) {
                return function (t) {
                    for (var r = [], n = 0; n < this.length; n++) {
                        var i = this[n],
                            a = i._private[e.attr];
                        a && r.push(a)
                    }
                    return this.spawn(r, {
                        unique: !0
                    }).filter(t)
                }
            }

            function i(e) {
                return function (t) {
                    var r = [],
                        n = this._private.cy,
                        i = e || {};
                    s.string(t) && (t = n.$(t));
                    for (var a = this._private.ids, o = t._private.ids, l = 0; l < t.length; l++)
                        for (var u = t[l]._private.edges, c = 0; c < u.length; c++) {
                            var d = u[c],
                                h = d._private.data,
                                p = a[h.source] && o[h.target],
                                f = o[h.source] && a[h.target],
                                v = p || f;
                            if (v) {
                                if (i.thisIsSrc || i.thisIsTgt) {
                                    if (i.thisIsSrc && !p) continue;
                                    if (i.thisIsTgt && !f) continue
                                }
                                r.push(d)
                            }
                        }
                    return this.spawn(r, {
                        unique: !0
                    })
                }
            }

            function a(e) {
                var t = {
                    codirected: !1
                };
                return e = o.extend({}, t, e),
                    function (t) {
                        for (var r = [], n = this.edges(), i = e, a = 0; a < n.length; a++)
                            for (var o = n[a], s = o._private, l = s.source, u = l._private.data.id, c = s.data.target, d = l._private.edges, h = 0; h < d.length; h++) {
                                var p = d[h],
                                    f = p._private.data,
                                    v = f.target,
                                    g = f.source,
                                    y = v === c && g === u,
                                    m = u === v && c === g;
                                (i.codirected && y || !i.codirected && (y || m)) && r.push(p)
                            }
                        return this.spawn(r, {
                            unique: !0
                        }).filter(t)
                    }
            }
            var o = e("../util"),
                s = e("../is"),
                l = {},
                u = function (e, t) {
                    return function (r, n, i, a) {
                        var o, l = r,
                            u = this;
                        if (null == l ? o = "null" : s.elementOrCollection(l) && 1 === l.length && (o = "#" + l.id()), 1 === u.length && o) {
                            var c = u[0]._private,
                                d = c.traversalCache = c.traversalCache || {},
                                h = d[t] = d[t] || {},
                                p = h[o];
                            return p ? p : h[o] = e.call(u, r, n, i, a)
                        }
                        return e.call(u, r, n, i, a)
                    }
                },
                c = function (e) {
                    return function (t) {
                        for (var r = this, n = [], i = 0; i < r.length; i++) {
                            var a = r[i];
                            if (a.isNode()) {
                                for (var o = !1, s = a.connectedEdges(), l = 0; l < s.length; l++) {
                                    var u = s[l],
                                        c = u.source(),
                                        d = u.target();
                                    if (e.noIncomingEdges && d === a && c !== a || e.noOutgoingEdges && c === a && d !== a) {
                                        o = !0;
                                        break
                                    }
                                }
                                o || n.push(a)
                            }
                        }
                        return this.spawn(n, {
                            unique: !0
                        }).filter(t)
                    }
                },
                d = function (e) {
                    return function (t) {
                        for (var r = this, n = [], i = 0; i < r.length; i++) {
                            var a = r[i];
                            if (a.isNode())
                                for (var o = a.connectedEdges(), s = 0; s < o.length; s++) {
                                    var l = o[s],
                                        u = l.source(),
                                        c = l.target();
                                    e.outgoing && u === a ? (n.push(l), n.push(c)) : e.incoming && c === a && (n.push(l), n.push(u))
                                }
                        }
                        return this.spawn(n, {
                            unique: !0
                        }).filter(t)
                    }
                },
                h = function (e) {
                    return function (t) {
                        for (var r = this, n = [], i = {}; ;) {
                            var a = e.outgoing ? r.outgoers() : r.incomers();
                            if (0 === a.length) break;
                            for (var o = !1, s = 0; s < a.length; s++) {
                                var l = a[s],
                                    u = l.id();
                                i[u] || (i[u] = !0, n.push(l), o = !0)
                            }
                            if (!o) break;
                            r = a
                        }
                        return this.spawn(n, {
                            unique: !0
                        }).filter(t)
                    }
                };
            o.extend(l, {
                roots: c({
                    noIncomingEdges: !0
                }),
                leaves: c({
                    noOutgoingEdges: !0
                }),
                outgoers: u(d({
                    outgoing: !0
                }), "outgoers"),
                successors: h({
                    outgoing: !0
                }),
                incomers: u(d({
                    incoming: !0
                }), "incomers"),
                predecessors: h({
                    incoming: !0
                })
            }), o.extend(l, {
                neighborhood: u(function (e) {
                    for (var t = [], r = this.nodes(), n = 0; n < r.length; n++)
                        for (var i = r[n], a = i.connectedEdges(), o = 0; o < a.length; o++) {
                            var s = a[o],
                                l = s.source(),
                                u = s.target(),
                                c = i === l ? u : l;
                            c.length > 0 && t.push(c[0]), t.push(s[0])
                        }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                }, "neighborhood"),
                closedNeighborhood: function (e) {
                    return this.neighborhood().add(this).filter(e)
                },
                openNeighborhood: function (e) {
                    return this.neighborhood(e)
                }
            }), l.neighbourhood = l.neighborhood, l.closedNeighbourhood = l.closedNeighborhood, l.openNeighbourhood = l.openNeighborhood, o.extend(l, {
                source: u(function (e) {
                    var t, r = this[0];
                    return r && (t = r._private.source || r.cy().collection()), t && e ? t.filter(e) : t
                }, "source"),
                target: u(function (e) {
                    var t, r = this[0];
                    return r && (t = r._private.target || r.cy().collection()), t && e ? t.filter(e) : t
                }, "target"),
                sources: n({
                    attr: "source"
                }),
                targets: n({
                    attr: "target"
                })
            }), o.extend(l, {
                edgesWith: u(i(), "edgesWith", !0),
                edgesTo: u(i({
                    thisIsSrc: !0
                }), "edgesTo", !0)
            }), o.extend(l, {
                connectedEdges: u(function (e) {
                    for (var t = [], r = this, n = 0; n < r.length; n++) {
                        var i = r[n];
                        if (i.isNode())
                            for (var a = i._private.edges, o = 0; o < a.length; o++) {
                                var s = a[o];
                                t.push(s)
                            }
                    }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                }, "connectedEdges"),
                connectedNodes: u(function (e) {
                    for (var t = [], r = this, n = 0; n < r.length; n++) {
                        var i = r[n];
                        i.isEdge() && (t.push(i.source()[0]), t.push(i.target()[0]))
                    }
                    return this.spawn(t, {
                        unique: !0
                    }).filter(e)
                }, "connectedNodes"),
                parallelEdges: u(a(), "parallelEdges"),
                codirectedEdges: u(a({
                    codirected: !0
                }), "codirectedEdges")
            }), o.extend(l, {
                components: function () {
                    var e = this,
                        t = e.cy(),
                        r = e.spawn(),
                        n = e.nodes().spawnSelf(),
                        i = [],
                        a = function (e, t) {
                            r.merge(e), n.unmerge(e), t.merge(e)
                        };
                    if (n.empty()) return e.spawn();
                    do {
                        var o = t.collection();
                        i.push(o);
                        var s = n[0];
                        a(s, o), e.bfs({
                            directed: !1,
                            roots: s,
                            visit: function (e, t, r, n, i) {
                                a(r, o)
                            }
                        })
                    } while (n.length > 0);
                    return i.map(function (e) {
                        var t = e.connectedEdges().stdFilter(function (t) {
                            return e.anySame(t.source()) && e.anySame(t.target())
                        });
                        return e.union(t)
                    })
                }
            }), t.exports = l
        }, {
            "../is": 83,
            "../util": 100
        }],
        32: [function (e, t, r) {
            "use strict";
            var n = function (e, t) {
                var r = e.cy(),
                    n = e.pstyle("z-index").value - t.pstyle("z-index").value,
                    i = 0,
                    a = 0,
                    o = r.hasCompoundNodes(),
                    s = e.isNode(),
                    l = !s,
                    u = t.isNode(),
                    c = !u;
                o && (i = e.zDepth(), a = t.zDepth());
                var d = i - a,
                    h = 0 === d;
                return h ? s && c ? 1 : l && u ? -1 : 0 === n ? e.poolIndex() - t.poolIndex() : n : d
            };
            t.exports = n
        }, {}],
        33: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../util"),
                a = e("../collection"),
                o = e("../collection/element"),
                s = {
                    add: function (e) {
                        var t, r = this;
                        if (n.elementOrCollection(e)) {
                            var s = e;
                            if (s._private.cy === r) t = s.restore();
                            else {
                                for (var l = [], u = 0; u < s.length; u++) {
                                    var c = s[u];
                                    l.push(c.json())
                                }
                                t = new a(r, l)
                            }
                        } else if (n.array(e)) {
                            var l = e;
                            t = new a(r, l)
                        } else if (n.plainObject(e) && (n.array(e.nodes) || n.array(e.edges))) {
                            for (var d = e, l = [], h = ["nodes", "edges"], u = 0, p = h.length; p > u; u++) {
                                var f = h[u],
                                    v = d[f];
                                if (n.array(v))
                                    for (var g = 0, y = v.length; y > g; g++) {
                                        var m = i.extend({
                                            group: f
                                        }, v[g]);
                                        l.push(m)
                                    }
                            }
                            t = new a(r, l)
                        } else {
                            var m = e;
                            t = new o(r, m).collection()
                        }
                        return t
                    },
                    remove: function (e) {
                        if (n.elementOrCollection(e));
                        else if (n.string(e)) {
                            var t = e;
                            e = this.$(t)
                        }
                        return e.remove()
                    },
                    load: function (e, t, r) {
                        var a = this;
                        a.notifications(!1);
                        var o = a.mutableElements();
                        o.length > 0 && o.remove(), null != e && (n.plainObject(e) || n.array(e)) && a.add(e), a.one("layoutready", function (e) {
                            a.notifications(!0), a.trigger(e), a.notify({
                                type: "load",
                                eles: a.mutableElements()
                            }), a.one("load", t), a.trigger("load")
                        }).one("layoutstop", function () {
                            a.one("done", r), a.trigger("done")
                        });
                        var s = i.extend({}, a._private.options.layout);
                        return s.eles = a.elements(), a.layout(s), this
                    }
                };
            t.exports = s
        }, {
            "../collection": 26,
            "../collection/element": 22,
            "../is": 83,
            "../util": 100
        }],
        34: [function (e, t, r) {
            "use strict";
            var n = e("../define"),
                i = e("../util"),
                a = e("../is"),
                o = {
                    animate: n.animate(),
                    animation: n.animation(),
                    animated: n.animated(),
                    clearQueue: n.clearQueue(),
                    delay: n.delay(),
                    delayAnimation: n.delayAnimation(),
                    stop: n.stop(),
                    addToAnimationPool: function (e) {
                        var t = this;
                        t.styleEnabled() && t._private.aniEles.merge(e)
                    },
                    stopAnimationLoop: function () {
                        this._private.animationsRunning = !1
                    },
                    startAnimationLoop: function () {
                        function e() {
                            c._private.animationsRunning && i.requestAnimationFrame(function (r) {
                                t(r), e()
                            })
                        }

                        function t(e) {
                            function t(t, i) {
                                var s = t._private,
                                    l = s.animation.current,
                                    u = s.animation.queue,
                                    c = !1;
                                if (0 === l.length) {
                                    var d = u.shift();
                                    d && l.push(d)
                                }
                                for (var h = function (e) {
                                    for (var t = e.length - 1; t >= 0; t--) {
                                        var r = e[t];
                                        r()
                                    }
                                    e.splice(0, e.length)
                                }, p = l.length - 1; p >= 0; p--) {
                                    var f = l[p],
                                        v = f._private;
                                    v.stopped ? (l.splice(p, 1), v.hooked = !1, v.playing = !1, v.started = !1, h(v.frames)) : (v.playing || v.applying) && (v.playing && v.applying && (v.applying = !1), v.started || r(t, f, e), n(t, f, e, i), a.fn(v.step) && v.step.call(t, e), v.applying && (v.applying = !1), h(v.frames), f.completed() && (l.splice(p, 1), v.hooked = !1, v.playing = !1, v.started = !1, h(v.completes)), c = !0)
                                }
                                return i || 0 !== l.length || 0 !== u.length || o.push(t), c
                            }
                            for (var i = c._private.aniEles, o = [], s = !1, l = 0; l < i.length; l++) {
                                var u = i[l],
                                    d = t(u);
                                s = s || d
                            }
                            var h = t(c, !0);
                            if (s || h)
                                if (i.length > 0) {
                                    var p = i.updateCompoundBounds().spawnSelf().merge(i);
                                    c.notify({
                                        type: "draw",
                                        eles: p
                                    })
                                } else c.notify({
                                    type: "draw"
                                });
                            i.unmerge(o), c.trigger("step")
                        }

                        function r(e, t, r) {
                            var n = a.core(e),
                                i = !n,
                                o = e,
                                s = c._private.style,
                                l = t._private;
                            if (i) {
                                var u = o._private.position;
                                l.startPosition = l.startPosition || {
                                    x: u.x,
                                    y: u.y
                                }, l.startStyle = l.startStyle || s.getAnimationStartStyle(o, l.style)
                            }
                            if (n) {
                                var d = c._private.pan;
                                l.startPan = l.startPan || {
                                    x: d.x,
                                    y: d.y
                                }, l.startZoom = null != l.startZoom ? l.startZoom : c._private.zoom
                            }
                            l.started = !0, l.startTime = r - l.progress * l.duration
                        }

                        function n(e, t, r, n) {
                            var i = c._private.style,
                                s = !n,
                                l = e._private,
                                d = t._private,
                                h = d.easing,
                                f = d.startTime;
                            if (!d.easingImpl)
                                if (null == h) d.easingImpl = p.linear;
                                else {
                                    var v;
                                    if (a.string(h)) {
                                        var g = i.parse("transition-timing-function", h);
                                        v = g.value
                                    } else v = h;
                                    var y, m;
                                    a.string(v) ? (y = v, m = []) : (y = v[1], m = v.slice(2).map(function (e) {
                                        return +e
                                    })), m.length > 0 ? ("spring" === y && m.push(d.duration), d.easingImpl = p[y].apply(null, m)) : d.easingImpl = p[y]
                                }
                            var b, x = d.easingImpl;
                            if (b = 0 === d.duration ? 1 : (r - f) / d.duration, d.applying && (b = d.progress), 0 > b ? b = 0 : b > 1 && (b = 1), null == d.delay) {
                                var w = d.startPosition,
                                    E = d.position,
                                    _ = l.position;
                                E && s && (o(w.x, E.x) && (_.x = u(w.x, E.x, b, x)), o(w.y, E.y) && (_.y = u(w.y, E.y, b, x)), e.trigger("position"));
                                var S = d.startPan,
                                    P = d.pan,
                                    T = l.pan,
                                    k = null != P && n;
                                k && (o(S.x, P.x) && (T.x = u(S.x, P.x, b, x)), o(S.y, P.y) && (T.y = u(S.y, P.y, b, x)), e.trigger("pan"));
                                var D = d.startZoom,
                                    C = d.zoom,
                                    M = null != C && n;
                                M && (o(D, C) && (l.zoom = u(D, C, b, x)), e.trigger("zoom")), (k || M) && e.trigger("viewport");
                                var N = d.style;
                                if (N && N.length > 0 && s) {
                                    for (var B = 0; B < N.length; B++) {
                                        var z = N[B],
                                            y = z.name,
                                            I = z,
                                            L = d.startStyle[y],
                                            O = u(L, I, b, x);
                                        i.overrideBypass(e, y, O)
                                    }
                                    e.trigger("style")
                                }
                            }
                            return d.progress = b, b
                        }

                        function o(e, t) {
                            return null == e || null == t ? !1 : a.number(e) && a.number(t) ? !0 : !(!e || !t)
                        }

                        function s(e, t, r) {
                            var n = 1 - r,
                                i = r * r;
                            return 3 * n * n * r * e + 3 * n * i * t + i * r
                        }

                        function l(e, t) {
                            return function (r, n, i) {
                                return r + (n - r) * s(e, t, i)
                            }
                        }

                        function u(e, t, r, n) {
                            0 > r ? r = 0 : r > 1 && (r = 1);
                            var i, o;
                            if (i = null != e.pfValue || null != e.value ? null != e.pfValue ? e.pfValue : e.value : e, o = null != t.pfValue || null != t.value ? null != t.pfValue ? t.pfValue : t.value : t, a.number(i) && a.number(o)) return n(i, o, r);
                            if (a.array(i) && a.array(o)) {
                                for (var s = [], l = 0; l < o.length; l++) {
                                    var u = i[l],
                                        c = o[l];
                                    if (null != u && null != c) {
                                        var d = n(u, c, r);
                                        e.roundValue && (d = Math.round(d)), s.push(d)
                                    } else s.push(c)
                                }
                                return s
                            }
                        }
                        var c = this;
                        if (c._private.animationsRunning = !0, c.styleEnabled()) {
                            var d = c.renderer();
                            d && d.beforeRender ? d.beforeRender(function (e, r) {
                                t(r)
                            }, d.beforeRenderPriorities.animations) : e();
                            /*! Runge-Kutta spring physics function generator. Adapted from Framer.js, copyright Koen Bok. MIT License: http://en.wikipedia.org/wiki/MIT_License */
                            /* Given a tension, friction, and duration, a simulation at 60FPS will first run without a defined duration in order to calculate the full path. A second pass
                                   then adjusts the time delta -- using the relation between actual time and duration -- to calculate the path for the duration-constrained animation. */
                            var h = function () {
                                function e(e) {
                                    return -e.tension * e.x - e.friction * e.v
                                }

                                function t(t, r, n) {
                                    var i = {
                                        x: t.x + n.dx * r,
                                        v: t.v + n.dv * r,
                                        tension: t.tension,
                                        friction: t.friction
                                    };
                                    return {
                                        dx: i.v,
                                        dv: e(i)
                                    }
                                }

                                function r(r, n) {
                                    var i = {
                                        dx: r.v,
                                        dv: e(r)
                                    },
                                        a = t(r, .5 * n, i),
                                        o = t(r, .5 * n, a),
                                        s = t(r, n, o),
                                        l = 1 / 6 * (i.dx + 2 * (a.dx + o.dx) + s.dx),
                                        u = 1 / 6 * (i.dv + 2 * (a.dv + o.dv) + s.dv);
                                    return r.x = r.x + l * n, r.v = r.v + u * n, r
                                }
                                return function n(e, t, i) {
                                    var a, o, s, l = {
                                        x: -1,
                                        v: 0,
                                        tension: null,
                                        friction: null
                                    },
                                        u = [0],
                                        c = 0,
                                        d = 1e-4,
                                        h = .016;
                                    for (e = parseFloat(e) || 500, t = parseFloat(t) || 20, i = i || null, l.tension = e, l.friction = t, a = null !== i, a ? (c = n(e, t), o = c / i * h) : o = h; ;)
                                        if (s = r(s || l, o), u.push(1 + s.x), c += 16, !(Math.abs(s.x) > d && Math.abs(s.v) > d)) break;
                                    return a ? function (e) {
                                        return u[e * (u.length - 1) | 0]
                                    } : c
                                }
                            }(),
                                p = {
                                    linear: function (e, t, r) {
                                        return e + (t - e) * r
                                    },
                                    ease: l(.25, .1, .25, 1),
                                    "ease-in": l(.42, 0, 1, 1),
                                    "ease-out": l(0, 0, .58, 1),
                                    "ease-in-out": l(.42, 0, .58, 1),
                                    "ease-in-sine": l(.47, 0, .745, .715),
                                    "ease-out-sine": l(.39, .575, .565, 1),
                                    "ease-in-out-sine": l(.445, .05, .55, .95),
                                    "ease-in-quad": l(.55, .085, .68, .53),
                                    "ease-out-quad": l(.25, .46, .45, .94),
                                    "ease-in-out-quad": l(.455, .03, .515, .955),
                                    "ease-in-cubic": l(.55, .055, .675, .19),
                                    "ease-out-cubic": l(.215, .61, .355, 1),
                                    "ease-in-out-cubic": l(.645, .045, .355, 1),
                                    "ease-in-quart": l(.895, .03, .685, .22),
                                    "ease-out-quart": l(.165, .84, .44, 1),
                                    "ease-in-out-quart": l(.77, 0, .175, 1),
                                    "ease-in-quint": l(.755, .05, .855, .06),
                                    "ease-out-quint": l(.23, 1, .32, 1),
                                    "ease-in-out-quint": l(.86, 0, .07, 1),
                                    "ease-in-expo": l(.95, .05, .795, .035),
                                    "ease-out-expo": l(.19, 1, .22, 1),
                                    "ease-in-out-expo": l(1, 0, 0, 1),
                                    "ease-in-circ": l(.6, .04, .98, .335),
                                    "ease-out-circ": l(.075, .82, .165, 1),
                                    "ease-in-out-circ": l(.785, .135, .15, .86),
                                    spring: function (e, t, r) {
                                        if (0 === r) return p.linear;
                                        var n = h(e, t, r);
                                        return function (e, t, r) {
                                            return e + (t - e) * n(r)
                                        }
                                    },
                                    "cubic-bezier": function (e, t, r, n) {
                                        return l(e, t, r, n)
                                    }
                                }
                        }
                    }
                };
            t.exports = o
        }, {
            "../define": 44,
            "../is": 83,
            "../util": 100
        }],
        35: [function (e, t, r) {
            "use strict";
            var n = e("../define"),
                i = {
                    on: n.on(),
                    one: n.on({
                        unbindSelfOnTrigger: !0
                    }),
                    once: n.on({
                        unbindAllBindersOnTrigger: !0
                    }),
                    off: n.off(),
                    trigger: n.trigger()
                };
            n.eventAliasesOn(i), t.exports = i
        }, {
            "../define": 44
        }],
        36: [function (e, t, r) {
            "use strict";
            var n = {
                png: function (e) {
                    var t = this._private.renderer;
                    return e = e || {}, t.png(e)
                },
                jpg: function (e) {
                    var t = this._private.renderer;
                    return e = e || {}, e.bg = e.bg || "#fff", t.jpg(e)
                }
            };
            n.jpeg = n.jpg, t.exports = n
        }, {}],
        37: [function (e, t, r) {
            "use strict";
            var n = e("../window"),
                i = e("../util"),
                a = e("../collection"),
                o = e("../is"),
                s = e("../promise"),
                l = e("../define"),
                u = function (e) {
                    var t = this;
                    e = i.extend({}, e);
                    var r = e.container;
                    r && !o.htmlElement(r) && o.htmlElement(r[0]) && (r = r[0]);
                    var l = r ? r._cyreg : null;
                    l = l || {}, l && l.cy && (l.cy.destroy(), l = {});
                    var u = l.readies = l.readies || [];
                    r && (r._cyreg = l), l.cy = t;
                    var c = void 0 !== n && void 0 !== r && !e.headless,
                        d = e;
                    d.layout = i.extend({
                        name: c ? "grid" : "null"
                    }, d.layout), d.renderer = i.extend({
                        name: c ? "canvas" : "null"
                    }, d.renderer);
                    var h = function (e, t, r) {
                        return void 0 !== t ? t : void 0 !== r ? r : e
                    },
                        p = this._private = {
                            container: r,
                            ready: !1,
                            initrender: !1,
                            options: d,
                            elements: new a(this),
                            listeners: [],
                            aniEles: new a(this),
                            scratch: {},
                            layout: null,
                            renderer: null,
                            notificationsEnabled: !0,
                            minZoom: 1e-50,
                            maxZoom: 1e50,
                            zoomingEnabled: h(!0, d.zoomingEnabled),
                            userZoomingEnabled: h(!0, d.userZoomingEnabled),
                            panningEnabled: h(!0, d.panningEnabled),
                            userPanningEnabled: h(!0, d.userPanningEnabled),
                            boxSelectionEnabled: h(!0, d.boxSelectionEnabled),
                            autolock: h(!1, d.autolock, d.autolockNodes),
                            autoungrabify: h(!1, d.autoungrabify, d.autoungrabifyNodes),
                            autounselectify: h(!1, d.autounselectify),
                            styleEnabled: void 0 === d.styleEnabled ? c : d.styleEnabled,
                            zoom: o.number(d.zoom) ? d.zoom : 1,
                            pan: {
                                x: o.plainObject(d.pan) && o.number(d.pan.x) ? d.pan.x : 0,
                                y: o.plainObject(d.pan) && o.number(d.pan.y) ? d.pan.y : 0
                            },
                            animation: {
                                current: [],
                                queue: []
                            },
                            hasCompoundNodes: !1
                        },
                        f = d.selectionType;
                    void 0 === f || "additive" !== f && "single" !== f ? p.selectionType = "single" : p.selectionType = f, o.number(d.minZoom) && o.number(d.maxZoom) && d.minZoom < d.maxZoom ? (p.minZoom = d.minZoom, p.maxZoom = d.maxZoom) : o.number(d.minZoom) && void 0 === d.maxZoom ? p.minZoom = d.minZoom : o.number(d.maxZoom) && void 0 === d.minZoom && (p.maxZoom = d.maxZoom);
                    var v = function (e, t) {
                        var r = e.some(o.promise);
                        return r ? s.all(e).then(t) : void t(e)
                    };
                    t.initRenderer(i.extend({
                        hideEdgesOnViewport: d.hideEdgesOnViewport,
                        textureOnViewport: d.textureOnViewport,
                        wheelSensitivity: o.number(d.wheelSensitivity) && d.wheelSensitivity > 0 ? d.wheelSensitivity : 1,
                        motionBlur: void 0 === d.motionBlur ? !1 : d.motionBlur,
                        motionBlurOpacity: void 0 === d.motionBlurOpacity ? .05 : d.motionBlurOpacity,
                        pixelRatio: o.number(d.pixelRatio) && d.pixelRatio > 0 ? d.pixelRatio : void 0,
                        desktopTapThreshold: void 0 === d.desktopTapThreshold ? 4 : d.desktopTapThreshold,
                        touchTapThreshold: void 0 === d.touchTapThreshold ? 8 : d.touchTapThreshold
                    }, d.renderer)), v([d.style, d.elements], function (e) {
                        var r = e[0],
                            n = e[1];
                        p.styleEnabled && t.setStyle(r), d.initrender && (t.on("initrender", d.initrender), t.on("initrender", function () {
                            p.initrender = !0
                        })), t.load(n, function () {
                            t.startAnimationLoop(), p.ready = !0, o.fn(d.ready) && t.on("ready", d.ready);
                            for (var e = 0; e < u.length; e++) {
                                var r = u[e];
                                t.on("ready", r)
                            }
                            l && (l.readies = []), t.trigger("ready")
                        }, d.done)
                    })
                },
                c = u.prototype;
            i.extend(c, {
                instanceString: function () {
                    return "core"
                },
                isReady: function () {
                    return this._private.ready
                },
                ready: function (e) {
                    return this.isReady() ? this.trigger("ready", [], e) : this.on("ready", e), this
                },
                initrender: function () {
                    return this._private.initrender
                },
                destroy: function () {
                    var e = this;
                    return e.stopAnimationLoop(), e.destroyRenderer(), e
                },
                hasElementWithId: function (e) {
                    return this._private.elements.hasElementWithId(e)
                },
                getElementById: function (e) {
                    return this._private.elements.getElementById(e)
                },
                selectionType: function () {
                    return this._private.selectionType
                },
                hasCompoundNodes: function () {
                    return this._private.hasCompoundNodes
                },
                headless: function () {
                    return "null" === this._private.options.renderer.name
                },
                styleEnabled: function () {
                    return this._private.styleEnabled
                },
                addToPool: function (e) {
                    return this._private.elements.merge(e), this
                },
                removeFromPool: function (e) {
                    return this._private.elements.unmerge(e), this
                },
                container: function () {
                    return this._private.container
                },
                options: function () {
                    return i.copy(this._private.options)
                },
                json: function (e) {
                    var t = this,
                        r = t._private,
                        n = t.mutableElements();
                    if (o.plainObject(e)) {
                        if (t.startBatch(), e.elements) {
                            var a = {},
                                s = function (e, r) {
                                    for (var n = 0; n < e.length; n++) {
                                        var o = e[n],
                                            s = o.data.id,
                                            l = t.getElementById(s);
                                        a[s] = !0, 0 !== l.length ? l.json(o) : r ? t.add(i.extend({
                                            group: r
                                        }, o)) : t.add(o)
                                    }
                                };
                            if (o.array(e.elements)) s(e.elements);
                            else
                                for (var l = ["nodes", "edges"], u = 0; u < l.length; u++) {
                                    var c = l[u],
                                        d = e.elements[c];
                                    o.array(d) && s(d, c)
                                }
                            n.stdFilter(function (e) {
                                return !a[e.id()]
                            }).remove()
                        }
                        e.style && t.style(e.style), null != e.zoom && e.zoom !== r.zoom && t.zoom(e.zoom), e.pan && (e.pan.x === r.pan.x && e.pan.y === r.pan.y || t.pan(e.pan));
                        for (var h = ["minZoom", "maxZoom", "zoomingEnabled", "userZoomingEnabled", "panningEnabled", "userPanningEnabled", "boxSelectionEnabled", "autolock", "autoungrabify", "autounselectify"], u = 0; u < h.length; u++) {
                            var p = h[u];
                            null != e[p] && t[p](e[p])
                        }
                        return t.endBatch(), this
                    }
                    if (void 0 === e) {
                        var f = {};
                        return f.elements = {}, n.forEach(function (e) {
                            var t = e.group();
                            f.elements[t] || (f.elements[t] = []), f.elements[t].push(e.json())
                        }), this._private.styleEnabled && (f.style = t.style().json()), f.zoomingEnabled = t._private.zoomingEnabled, f.userZoomingEnabled = t._private.userZoomingEnabled, f.zoom = t._private.zoom, f.minZoom = t._private.minZoom, f.maxZoom = t._private.maxZoom, f.panningEnabled = t._private.panningEnabled, f.userPanningEnabled = t._private.userPanningEnabled, f.pan = i.copy(t._private.pan), f.boxSelectionEnabled = t._private.boxSelectionEnabled, f.renderer = i.copy(t._private.options.renderer), f.hideEdgesOnViewport = t._private.options.hideEdgesOnViewport, f.textureOnViewport = t._private.options.textureOnViewport, f.wheelSensitivity = t._private.options.wheelSensitivity, f.motionBlur = t._private.options.motionBlur, f
                    }
                },
                scratch: l.data({
                    field: "scratch",
                    bindingEvent: "scratch",
                    allowBinding: !0,
                    allowSetting: !0,
                    settingEvent: "scratch",
                    settingTriggersEvent: !0,
                    triggerFnName: "trigger",
                    allowGetting: !0
                }),
                removeScratch: l.removeData({
                    field: "scratch",
                    event: "scratch",
                    triggerFnName: "trigger",
                    triggerEvent: !0
                })
            }), [e("./add-remove"), e("./animation"), e("./events"), e("./export"), e("./layout"), e("./notification"), e("./renderer"), e("./search"), e("./style"), e("./viewport")].forEach(function (e) {
                i.extend(c, e)
            }), t.exports = u
        }, {
            "../collection": 26,
            "../define": 44,
            "../is": 83,
            "../promise": 86,
            "../util": 100,
            "../window": 107,
            "./add-remove": 33,
            "./animation": 34,
            "./events": 35,
            "./export": 36,
            "./layout": 38,
            "./notification": 39,
            "./renderer": 40,
            "./search": 41,
            "./style": 42,
            "./viewport": 43
        }],
        38: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = {
                    layout: function (e) {
                        var t = this._private.prevLayout = null == e ? this._private.prevLayout : this.makeLayout(e);
                        return t.run(), this
                    },
                    makeLayout: function (e) {
                        var t = this;
                        if (null == e) return void n.error("Layout options must be specified to make a layout");
                        if (null == e.name) return void n.error("A `name` must be specified to make a layout");
                        var r = e.name,
                            a = t.extension("layout", r);
                        if (null == a) return void n.error("Can not apply layout: No such layout `" + r + "` found; did you include its JS file?");
                        var o;
                        o = i.string(e.eles) ? t.$(e.eles) : null != e.eles ? e.eles : t.$();
                        var s = new a(n.extend({}, e, {
                            cy: t,
                            eles: o
                        }));
                        return s
                    }
                };
            a.createLayout = a.makeLayout, t.exports = a
        }, {
            "../is": 83,
            "../util": 100
        }],
        39: [function (e, t, r) {
            "use strict";
            var n = {
                notify: function (e) {
                    var t = this._private;
                    if (t.batchingNotify) {
                        var r = t.batchNotifyEles,
                            n = t.batchNotifyTypes;
                        return e.eles && r.merge(e.eles), void (n.ids[e.type] || (n.push(e.type), n.ids[e.type] = !0))
                    }
                    if (t.notificationsEnabled) {
                        var i = this.renderer();
                        i && i.notify(e)
                    }
                },
                notifications: function (e) {
                    var t = this._private;
                    return void 0 === e ? t.notificationsEnabled : void (t.notificationsEnabled = !!e)
                },
                noNotifications: function (e) {
                    this.notifications(!1), e(), this.notifications(!0)
                },
                startBatch: function () {
                    var e = this._private;
                    return null == e.batchCount && (e.batchCount = 0), 0 === e.batchCount && (e.batchingStyle = e.batchingNotify = !0, e.batchStyleEles = this.collection(), e.batchNotifyEles = this.collection(), e.batchNotifyTypes = [], e.batchNotifyTypes.ids = {}), e.batchCount++ , this
                },
                endBatch: function () {
                    var e = this._private;
                    return e.batchCount-- , 0 === e.batchCount && (e.batchingStyle = !1, e.batchStyleEles.updateStyle(), e.batchingNotify = !1, this.notify({
                        type: e.batchNotifyTypes,
                        eles: e.batchNotifyEles
                    })), this
                },
                batch: function (e) {
                    return this.startBatch(), e(), this.endBatch(), this
                },
                batchData: function (e) {
                    var t = this;
                    return this.batch(function () {
                        for (var r = Object.keys(e), n = 0; n < r.length; n++) {
                            var i = r[n],
                                a = e[i],
                                o = t.getElementById(i);
                            o.data(a)
                        }
                    })
                }
            };
            t.exports = n
        }, {}],
        40: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = {
                    renderTo: function (e, t, r, n) {
                        var i = this._private.renderer;
                        return i.renderTo(e, t, r, n), this
                    },
                    renderer: function () {
                        return this._private.renderer
                    },
                    forceRender: function () {
                        return this.notify({
                            type: "draw"
                        }), this
                    },
                    resize: function () {
                        return this.invalidateSize(), this.notify({
                            type: "resize"
                        }), this.trigger("resize"), this
                    },
                    initRenderer: function (e) {
                        var t = this,
                            r = t.extension("renderer", e.name);
                        if (null == r) return void n.error("Can not initialise: No such renderer `%s` found; did you include its JS file?", e.name);
                        var i = n.extend({}, e, {
                            cy: t
                        });
                        t._private.renderer = new r(i)
                    },
                    destroyRenderer: function () {
                        var e = this;
                        e.notify({
                            type: "destroy"
                        });
                        var t = e.container();
                        if (t)
                            for (t._cyreg = null; t.childNodes.length > 0;) t.removeChild(t.childNodes[0]);
                        e._private.renderer = null
                    },
                    onRender: function (e) {
                        return this.on("render", e)
                    },
                    offRender: function (e) {
                        return this.off("render", e)
                    }
                };
            i.invalidateDimensions = i.resize, t.exports = i
        }, {
            "../util": 100
        }],
        41: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../collection"),
                a = {
                    collection: function (e, t) {
                        return n.string(e) ? this.$(e) : n.elementOrCollection(e) ? e.collection() : n.array(e) ? new i(this, e, t) : new i(this)
                    },
                    nodes: function (e) {
                        var t = this.$(function () {
                            return this.isNode()
                        });
                        return e ? t.filter(e) : t
                    },
                    edges: function (e) {
                        var t = this.$(function () {
                            return this.isEdge()
                        });
                        return e ? t.filter(e) : t
                    },
                    $: function (e) {
                        var t = this._private.elements;
                        return e ? t.filter(e) : t.spawnSelf()
                    },
                    mutableElements: function () {
                        return this._private.elements
                    }
                };
            a.elements = a.filter = a.$, t.exports = a
        }, {
            "../collection": 26,
            "../is": 83
        }],
        42: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../style"),
                a = {
                    style: function (e) {
                        if (e) {
                            var t = this.setStyle(e);
                            t.update()
                        }
                        return this._private.style
                    },
                    setStyle: function (e) {
                        var t = this._private;
                        return n.stylesheet(e) ? t.style = e.generateStyle(this) : n.array(e) ? t.style = i.fromJson(this, e) : n.string(e) ? t.style = i.fromString(this, e) : t.style = i(this), t.style
                    }
                };
            t.exports = a
        }, {
            "../is": 83,
            "../style": 92
        }],
        43: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../window"),
                a = {
                    autolock: function (e) {
                        return void 0 === e ? this._private.autolock : (this._private.autolock = !!e, this)
                    },
                    autoungrabify: function (e) {
                        return void 0 === e ? this._private.autoungrabify : (this._private.autoungrabify = !!e, this)
                    },
                    autounselectify: function (e) {
                        return void 0 === e ? this._private.autounselectify : (this._private.autounselectify = !!e, this)
                    },
                    panningEnabled: function (e) {
                        return void 0 === e ? this._private.panningEnabled : (this._private.panningEnabled = !!e, this)
                    },
                    userPanningEnabled: function (e) {
                        return void 0 === e ? this._private.userPanningEnabled : (this._private.userPanningEnabled = !!e, this)
                    },
                    zoomingEnabled: function (e) {
                        return void 0 === e ? this._private.zoomingEnabled : (this._private.zoomingEnabled = !!e, this)
                    },
                    userZoomingEnabled: function (e) {
                        return void 0 === e ? this._private.userZoomingEnabled : (this._private.userZoomingEnabled = !!e, this)
                    },
                    boxSelectionEnabled: function (e) {
                        return void 0 === e ? this._private.boxSelectionEnabled : (this._private.boxSelectionEnabled = !!e, this)
                    },
                    pan: function () {
                        var e, t, r, i, a, o = arguments,
                            s = this._private.pan;
                        switch (o.length) {
                            case 0:
                                return s;
                            case 1:
                                if (n.string(o[0])) return e = o[0], s[e];
                                if (n.plainObject(o[0])) {
                                    if (!this._private.panningEnabled) return this;
                                    r = o[0], i = r.x, a = r.y, n.number(i) && (s.x = i), n.number(a) && (s.y = a), this.trigger("pan viewport")
                                }
                                break;
                            case 2:
                                if (!this._private.panningEnabled) return this;
                                e = o[0], t = o[1], "x" !== e && "y" !== e || !n.number(t) || (s[e] = t), this.trigger("pan viewport")
                        }
                        return this.notify({
                            type: "viewport"
                        }), this
                    },
                    panBy: function (e) {
                        var t, r, i, a, o, s = arguments,
                            l = this._private.pan;
                        if (!this._private.panningEnabled) return this;
                        switch (s.length) {
                            case 1:
                                n.plainObject(s[0]) && (i = s[0], a = i.x, o = i.y, n.number(a) && (l.x += a), n.number(o) && (l.y += o), this.trigger("pan viewport"));
                                break;
                            case 2:
                                t = s[0], r = s[1], "x" !== t && "y" !== t || !n.number(r) || (l[t] += r), this.trigger("pan viewport")
                        }
                        return this.notify({
                            type: "viewport"
                        }), this
                    },
                    fit: function (e, t) {
                        var r = this.getFitViewport(e, t);
                        if (r) {
                            var n = this._private;
                            n.zoom = r.zoom, n.pan = r.pan, this.trigger("pan zoom viewport"), this.notify({
                                type: "viewport"
                            })
                        }
                        return this
                    },
                    getFitViewport: function (e, t) {
                        if (n.number(e) && void 0 === t && (t = e, e = void 0), this._private.panningEnabled && this._private.zoomingEnabled) {
                            var r;
                            if (n.string(e)) {
                                var i = e;
                                e = this.$(i)
                            } else if (n.boundingBox(e)) {
                                var a = e;
                                r = {
                                    x1: a.x1,
                                    y1: a.y1,
                                    x2: a.x2,
                                    y2: a.y2
                                }, r.w = r.x2 - r.x1, r.h = r.y2 - r.y1
                            } else n.elementOrCollection(e) || (e = this.mutableElements());
                            r = r || e.boundingBox();
                            var o, s = this.width(),
                                l = this.height();
                            if (t = n.number(t) ? t : 0, !isNaN(s) && !isNaN(l) && s > 0 && l > 0 && !isNaN(r.w) && !isNaN(r.h) && r.w > 0 && r.h > 0) {
                                o = Math.min((s - 2 * t) / r.w, (l - 2 * t) / r.h), o = o > this._private.maxZoom ? this._private.maxZoom : o, o = o < this._private.minZoom ? this._private.minZoom : o;
                                var u = {
                                    x: (s - o * (r.x1 + r.x2)) / 2,
                                    y: (l - o * (r.y1 + r.y2)) / 2
                                };
                                return {
                                    zoom: o,
                                    pan: u
                                }
                            }
                        }
                    },
                    minZoom: function (e) {
                        return void 0 === e ? this._private.minZoom : (n.number(e) && (this._private.minZoom = e), this)
                    },
                    maxZoom: function (e) {
                        return void 0 === e ? this._private.maxZoom : (n.number(e) && (this._private.maxZoom = e), this)
                    },
                    zoom: function (e) {
                        var t, r;
                        if (void 0 === e) return this._private.zoom;
                        if (n.number(e)) r = e;
                        else if (n.plainObject(e)) {
                            if (r = e.level, e.position) {
                                var i = e.position,
                                    a = this._private.pan,
                                    o = this._private.zoom;
                                t = {
                                    x: i.x * o + a.x,
                                    y: i.y * o + a.y
                                }
                            } else e.renderedPosition && (t = e.renderedPosition);
                            if (t && !this._private.panningEnabled) return this
                        }
                        if (!this._private.zoomingEnabled) return this;
                        if (!n.number(r) || t && (!n.number(t.x) || !n.number(t.y))) return this;
                        if (r = r > this._private.maxZoom ? this._private.maxZoom : r, r = r < this._private.minZoom ? this._private.minZoom : r, t) {
                            var s = this._private.pan,
                                l = this._private.zoom,
                                u = r,
                                c = {
                                    x: -u / l * (t.x - s.x) + t.x,
                                    y: -u / l * (t.y - s.y) + t.y
                                };
                            this._private.zoom = r, this._private.pan = c;
                            var d = s.x !== c.x || s.y !== c.y;
                            this.trigger(" zoom " + (d ? " pan " : "") + " viewport ")
                        } else this._private.zoom = r, this.trigger("zoom viewport");
                        return this.notify({
                            type: "viewport"
                        }), this
                    },
                    viewport: function (e) {
                        var t = this._private,
                            r = !0,
                            i = !0,
                            a = [],
                            o = !1,
                            s = !1;
                        if (!e) return this;
                        if (n.number(e.zoom) || (r = !1), n.plainObject(e.pan) || (i = !1), !r && !i) return this;
                        if (r) {
                            var l = e.zoom;
                            l < t.minZoom || l > t.maxZoom || !t.zoomingEnabled ? o = !0 : (t.zoom = l, a.push("zoom"))
                        }
                        if (i && (!o || !e.cancelOnFailedZoom) && t.panningEnabled) {
                            var u = e.pan;
                            n.number(u.x) && (t.pan.x = u.x, s = !1), n.number(u.y) && (t.pan.y = u.y, s = !1), s || a.push("pan")
                        }
                        return a.length > 0 && (a.push("viewport"), this.trigger(a.join(" ")), this.notify({
                            type: "viewport"
                        })), this
                    },
                    center: function (e) {
                        var t = this.getCenterPan(e);
                        return t && (this._private.pan = t, this.trigger("pan viewport"), this.notify({
                            type: "viewport"
                        })), this
                    },
                    getCenterPan: function (e, t) {
                        if (this._private.panningEnabled) {
                            if (n.string(e)) {
                                var r = e;
                                e = this.mutableElements().filter(r)
                            } else n.elementOrCollection(e) || (e = this.mutableElements());
                            var i = e.boundingBox(),
                                a = this.width(),
                                o = this.height();
                            t = void 0 === t ? this._private.zoom : t;
                            var s = {
                                x: (a - t * (i.x1 + i.x2)) / 2,
                                y: (o - t * (i.y1 + i.y2)) / 2
                            };
                            return s
                        }
                    },
                    reset: function () {
                        return this._private.panningEnabled && this._private.zoomingEnabled ? (this.viewport({
                            pan: {
                                x: 0,
                                y: 0
                            },
                            zoom: 1
                        }), this) : this
                    },
                    invalidateSize: function () {
                        this._private.sizeCache = null
                    },
                    size: function () {
                        var e = this._private,
                            t = e.container;
                        return e.sizeCache = e.sizeCache || (t ? function () {
                            var e = t.getBoundingClientRect(),
                                r = i.getComputedStyle(t),
                                n = function (e) {
                                    return parseFloat(r.getPropertyValue(e))
                                };
                            return {
                                width: e.width - n("padding-left") - n("padding-right") - n("border-left-width") - n("border-right-width"),
                                height: e.height - n("padding-top") - n("padding-bottom") - n("border-top-width") - n("border-bottom-width")
                            }
                        }() : {
                                width: 1,
                                height: 1
                            })
                    },
                    width: function () {
                        return this.size().width
                    },
                    height: function () {
                        return this.size().height
                    },
                    extent: function () {
                        var e = this._private.pan,
                            t = this._private.zoom,
                            r = this.renderedExtent(),
                            n = {
                                x1: (r.x1 - e.x) / t,
                                x2: (r.x2 - e.x) / t,
                                y1: (r.y1 - e.y) / t,
                                y2: (r.y2 - e.y) / t
                            };
                        return n.w = n.x2 - n.x1, n.h = n.y2 - n.y1, n
                    },
                    renderedExtent: function () {
                        var e = this.width(),
                            t = this.height();
                        return {
                            x1: 0,
                            y1: 0,
                            x2: e,
                            y2: t,
                            w: e,
                            h: t
                        }
                    }
                };
            a.centre = a.center, a.autolockNodes = a.autolock, a.autoungrabifyNodes = a.autoungrabify, t.exports = a
        }, {
            "../is": 83,
            "../window": 107
        }],
        44: [function (e, t, r) {
            "use strict";
            var n = e("./util"),
                i = e("./is"),
                a = e("./selector"),
                o = e("./promise"),
                s = e("./event"),
                l = e("./animation"),
                u = {
                    data: function (e) {
                        var t = {
                            field: "data",
                            bindingEvent: "data",
                            allowBinding: !1,
                            allowSetting: !1,
                            allowGetting: !1,
                            settingEvent: "data",
                            settingTriggersEvent: !1,
                            triggerFnName: "trigger",
                            immutableKeys: {},
                            updateStyle: !1,
                            onSet: function (e) { },
                            canSet: function (e) {
                                return !0
                            }
                        };
                        return e = n.extend({}, t, e),
                            function (t, r) {
                                var n = e,
                                    a = this,
                                    o = void 0 !== a.length,
                                    s = o ? a : [a],
                                    l = o ? a[0] : a;
                                if (i.string(t)) {
                                    if (n.allowGetting && void 0 === r) {
                                        var u;
                                        return l && (u = l._private[n.field][t]), u
                                    }
                                    if (n.allowSetting && void 0 !== r) {
                                        var c = !n.immutableKeys[t];
                                        if (c) {
                                            for (var d = 0, h = s.length; h > d; d++) n.canSet(s[d]) && (s[d]._private[n.field][t] = r);
                                            n.updateStyle && a.updateStyle(), n.onSet(a), n.settingTriggersEvent && a[n.triggerFnName](n.settingEvent)
                                        }
                                    }
                                } else if (n.allowSetting && i.plainObject(t)) {
                                    for (var p, f, v = t, g = Object.keys(v), d = 0; d < g.length; d++) {
                                        p = g[d], f = v[p];
                                        var c = !n.immutableKeys[p];
                                        if (c)
                                            for (var y = 0; y < s.length; y++) {
                                                var m = s[y];
                                                n.canSet(m) && (m._private[n.field][p] = f)
                                            }
                                    }
                                    n.updateStyle && a.updateStyle(), n.onSet(a), n.settingTriggersEvent && a[n.triggerFnName](n.settingEvent)
                                } else if (n.allowBinding && i.fn(t)) {
                                    var b = t;
                                    a.on(n.bindingEvent, b)
                                } else if (n.allowGetting && void 0 === t) {
                                    var u;
                                    return l && (u = l._private[n.field]), u
                                }
                                return a
                            }
                    },
                    removeData: function (e) {
                        var t = {
                            field: "data",
                            event: "data",
                            triggerFnName: "trigger",
                            triggerEvent: !1,
                            immutableKeys: {}
                        };
                        return e = n.extend({}, t, e),
                            function (t) {
                                var r = e,
                                    n = this,
                                    a = void 0 !== n.length,
                                    o = a ? n : [n];
                                if (i.string(t)) {
                                    for (var s = t.split(/\s+/), l = s.length, u = 0; l > u; u++) {
                                        var c = s[u];
                                        if (!i.emptyString(c)) {
                                            var d = !r.immutableKeys[c];
                                            if (d)
                                                for (var h = 0, p = o.length; p > h; h++) o[h]._private[r.field][c] = void 0
                                        }
                                    }
                                    r.triggerEvent && n[r.triggerFnName](r.event)
                                } else if (void 0 === t) {
                                    for (var h = 0, p = o.length; p > h; h++)
                                        for (var f = o[h]._private[r.field], s = Object.keys(f), u = 0; u < s.length; u++) {
                                            var c = s[u],
                                                v = !r.immutableKeys[c];
                                            v && (f[c] = void 0)
                                        }
                                    r.triggerEvent && n[r.triggerFnName](r.event)
                                }
                                return n
                            }
                    },
                    event: {
                        regex: /(\w+)(\.(?:\w+|\*))?/,
                        universalNamespace: ".*",
                        optionalTypeRegex: /(\w+)?(\.(?:\w+|\*))?/,
                        falseCallback: function () {
                            return !1
                        }
                    },
                    on: function (e) {
                        var t = {
                            unbindSelfOnTrigger: !1,
                            unbindAllBindersOnTrigger: !1
                        };
                        return e = n.extend({}, t, e),
                            function (t, r, n, o) {
                                var s = this,
                                    l = void 0 !== s.length,
                                    c = l ? s : [s],
                                    d = i.string(t),
                                    h = e;
                                if (i.plainObject(r) ? (o = n, n = r, r = void 0) : (i.fn(r) || r === !1) && (o = r, n = void 0, r = void 0), (i.fn(n) || n === !1) && (o = n, n = void 0), !i.fn(o) && o !== !1 && d) return s;
                                if (d) {
                                    var p = {};
                                    p[t] = o, t = p
                                }
                                for (var f = Object.keys(t), v = 0; v < f.length; v++) {
                                    var g = f[v];
                                    if (o = t[g], o === !1 && (o = u.event.falseCallback), i.fn(o)) {
                                        g = g.split(/\s+/);
                                        for (var y = 0; y < g.length; y++) {
                                            var m = g[y];
                                            if (!i.emptyString(m)) {
                                                var b = m.match(u.event.regex);
                                                if (b)
                                                    for (var x = b[1], w = b[2] ? b[2] : void 0, E = {
                                                        callback: o,
                                                        data: n,
                                                        delegated: !!r,
                                                        selector: r,
                                                        selObj: new a(r),
                                                        type: x,
                                                        namespace: w,
                                                        unbindSelfOnTrigger: h.unbindSelfOnTrigger,
                                                        unbindAllBindersOnTrigger: h.unbindAllBindersOnTrigger,
                                                        binders: c
                                                    }, _ = 0; _ < c.length; _++) {
                                                        var S = c[_]._private = c[_]._private || {};
                                                        S.listeners = S.listeners || [], S.listeners.push(E)
                                                    }
                                            }
                                        }
                                    }
                                }
                                return s
                            }
                    },
                    eventAliasesOn: function (e) {
                        var t = e;
                        t.addListener = t.listen = t.bind = t.on, t.removeListener = t.unlisten = t.unbind = t.off, t.emit = t.trigger, t.pon = t.promiseOn = function (e, t) {
                            var r = this,
                                n = Array.prototype.slice.call(arguments, 0);
                            return new o(function (e, t) {
                                var i = function (t) {
                                    r.off.apply(r, o), e(t)
                                },
                                    a = n.concat([i]),
                                    o = a.concat([]);
                                r.on.apply(r, a)
                            })
                        }
                    },
                    off: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t, r) {
                                var n = this,
                                    a = void 0 !== n.length,
                                    o = a ? n : [n],
                                    s = i.string(e);
                                if (0 === arguments.length) {
                                    for (var l = 0; l < o.length; l++) o[l]._private = o[l]._private || {}, b.listeners = [];
                                    return n
                                }
                                if ((i.fn(t) || t === !1) && (r = t, t = void 0), s) {
                                    var c = {};
                                    c[e] = r, e = c
                                }
                                for (var d = Object.keys(e), h = 0; h < d.length; h++) {
                                    var p = d[h];
                                    r = e[p], r === !1 && (r = u.event.falseCallback), p = p.split(/\s+/);
                                    for (var f = 0; f < p.length; f++) {
                                        var v = p[f];
                                        if (!i.emptyString(v)) {
                                            var g = v.match(u.event.optionalTypeRegex);
                                            if (g)
                                                for (var y = g[1] ? g[1] : void 0, m = g[2] ? g[2] : void 0, l = 0; l < o.length; l++)
                                                    for (var b = o[l]._private = o[l]._private || {}, x = b.listeners = b.listeners || [], w = 0; w < x.length; w++) {
                                                        var E = x[w],
                                                            _ = !m || m === E.namespace,
                                                            S = !y || E.type === y,
                                                            P = !r || r === E.callback,
                                                            T = _ && S && P;
                                                        T && (x.splice(w, 1), w--)
                                                    }
                                        }
                                    }
                                }
                                return n
                            }
                    },
                    trigger: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (t, r, n) {
                                var a = this,
                                    o = void 0 !== a.length,
                                    l = o ? a : [a],
                                    c = i.string(t),
                                    d = i.plainObject(t),
                                    h = i.event(t),
                                    p = this._private = this._private || {},
                                    f = p.cy || (i.core(this) ? this : null),
                                    v = f ? f.hasCompoundNodes() : !1;
                                if (c) {
                                    var g = t.split(/\s+/);
                                    t = [];
                                    for (var y = 0; y < g.length; y++) {
                                        var m = g[y];
                                        if (!i.emptyString(m)) {
                                            var b = m.match(u.event.regex),
                                                x = b[1],
                                                w = b[2] ? b[2] : void 0;
                                            t.push({
                                                type: x,
                                                namespace: w
                                            })
                                        }
                                    }
                                } else if (d) {
                                    var E = t;
                                    t = [E]
                                }
                                r ? i.array(r) || (r = [r]) : r = [];
                                for (var y = 0; y < t.length; y++)
                                    for (var _ = t[y], S = 0; S < l.length; S++) {
                                        var m, P = l[S],
                                            p = P._private = P._private || {},
                                            T = p.listeners = p.listeners || [],
                                            k = i.element(P),
                                            D = k || e.layout;
                                        if (h ? (m = _, m.cyTarget = m.cyTarget || P, m.cy = m.cy || f) : m = new s(_, {
                                            cyTarget: P,
                                            cy: f,
                                            namespace: _.namespace
                                        }), _.layout && (m.layout = _.layout), e.layout && (m.layout = P), m.cyPosition) {
                                            var C = m.cyPosition,
                                                M = f.zoom(),
                                                N = f.pan();
                                            m.cyRenderedPosition = {
                                                x: C.x * M + N.x,
                                                y: C.y * M + N.y
                                            }
                                        }
                                        n && (T = [{
                                            namespace: m.namespace,
                                            type: m.type,
                                            callback: n
                                        }]);
                                        for (var B = 0; B < T.length; B++) {
                                            var z = T[B],
                                                I = !z.namespace || z.namespace === m.namespace || z.namespace === u.event.universalNamespace,
                                                L = z.type === m.type,
                                                O = z.delegated ? P !== m.cyTarget && i.element(m.cyTarget) && z.selObj.matches(m.cyTarget) : !0,
                                                A = I && L && O;
                                            if (A) {
                                                var R = [m];
                                                if (R = R.concat(r), z.data ? m.data = z.data : m.data = void 0, (z.unbindSelfOnTrigger || z.unbindAllBindersOnTrigger) && (T.splice(B, 1), B--), z.unbindAllBindersOnTrigger)
                                                    for (var q = z.binders, V = 0; V < q.length; V++) {
                                                        var F = q[V];
                                                        if (F && F !== P)
                                                            for (var j = F._private.listeners, X = 0; X < j.length; X++) {
                                                                var Y = j[X];
                                                                Y === z && (j.splice(X, 1), X--)
                                                            }
                                                    }
                                                var W = z.delegated ? m.cyTarget : P,
                                                    $ = z.callback.apply(W, R);
                                                ($ === !1 || m.isPropagationStopped()) && (D = !1, $ === !1 && (m.stopPropagation(), m.preventDefault()))
                                            }
                                        }
                                        if (D) {
                                            var H = v ? P._private.parent : null,
                                                U = null != H && 0 !== H.length;
                                            U ? (H = H[0], H.trigger(m)) : f.trigger(m)
                                        }
                                    }
                                return a
                            }
                    },
                    animated: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function () {
                                var e = this,
                                    t = void 0 !== e.length,
                                    r = t ? e : [e],
                                    n = this._private.cy || this;
                                if (!n.styleEnabled()) return !1;
                                var i = r[0];
                                return i ? i._private.animation.current.length > 0 : void 0
                            }
                    },
                    clearQueue: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function () {
                                var e = this,
                                    t = void 0 !== e.length,
                                    r = t ? e : [e],
                                    n = this._private.cy || this;
                                if (!n.styleEnabled()) return this;
                                for (var i = 0; i < r.length; i++) {
                                    var a = r[i];
                                    a._private.animation.queue = []
                                }
                                return this
                            }
                    },
                    delay: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t) {
                                var r = this._private.cy || this;
                                return r.styleEnabled() ? this.animate({
                                    delay: e,
                                    duration: e,
                                    complete: t
                                }) : this
                            }
                    },
                    delayAnimation: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t) {
                                var r = this._private.cy || this;
                                return r.styleEnabled() ? this.animation({
                                    delay: e,
                                    duration: e,
                                    complete: t
                                }) : this
                            }
                    },
                    animation: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t) {
                                var r = this,
                                    i = void 0 !== r.length,
                                    a = i ? r : [r],
                                    o = this._private.cy || this,
                                    s = !i,
                                    u = !s;
                                if (!o.styleEnabled()) return this;
                                var c = o.style();
                                switch (e = n.extend({}, e, t), void 0 === e.duration && (e.duration = 400), e.duration) {
                                    case "slow":
                                        e.duration = 600;
                                        break;
                                    case "fast":
                                        e.duration = 200
                                }
                                var d = 0 === Object.keys(e).length;
                                if (d) return new l(a[0], e);
                                if (u && (e.style = c.getPropsList(e.style || e.css), e.css = void 0), e.renderedPosition && u) {
                                    var h = e.renderedPosition,
                                        p = o.pan(),
                                        f = o.zoom();
                                    e.position = {
                                        x: (h.x - p.x) / f,
                                        y: (h.y - p.y) / f
                                    }
                                }
                                if (e.panBy && s) {
                                    var v = e.panBy,
                                        g = o.pan();
                                    e.pan = {
                                        x: g.x + v.x,
                                        y: g.y + v.y
                                    }
                                }
                                var y = e.center || e.centre;
                                if (y && s) {
                                    var m = o.getCenterPan(y.eles, e.zoom);
                                    m && (e.pan = m)
                                }
                                if (e.fit && s) {
                                    var b = e.fit,
                                        x = o.getFitViewport(b.eles || b.boundingBox, b.padding);
                                    x && (e.pan = x.pan, e.zoom = x.zoom)
                                }
                                return new l(a[0], e)
                            }
                    },
                    animate: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t) {
                                var r = this,
                                    i = void 0 !== r.length,
                                    a = i ? r : [r],
                                    o = this._private.cy || this;
                                if (!o.styleEnabled()) return this;
                                t && (e = n.extend({}, e, t));
                                for (var s = 0; s < a.length; s++) {
                                    var l = a[s],
                                        u = l.animated() && (void 0 === e.queue || e.queue),
                                        c = l.animation(e, u ? {
                                            queue: !0
                                        } : void 0);
                                    c.play()
                                }
                                return this
                            }
                    },
                    stop: function (e) {
                        var t = {};
                        return e = n.extend({}, t, e),
                            function (e, t) {
                                var r = this,
                                    n = void 0 !== r.length,
                                    i = n ? r : [r],
                                    a = this._private.cy || this;
                                if (!a.styleEnabled()) return this;
                                for (var o = 0; o < i.length; o++) {
                                    for (var s = i[o], l = s._private, u = l.animation.current, c = 0; c < u.length; c++) {
                                        var d = u[c],
                                            h = d._private;
                                        t && (h.duration = 0)
                                    }
                                    e && (l.animation.queue = []), t || (l.animation.current = [])
                                }
                                return a.notify({
                                    eles: this,
                                    type: "draw"
                                }), this
                            }
                    }
                };
            t.exports = u
        }, {
            "./animation": 2,
            "./event": 45,
            "./is": 83,
            "./promise": 86,
            "./selector": 87,
            "./util": 100
        }],
        45: [function (e, t, r) {
            "use strict";

            function n() {
                return !1
            }

            function i() {
                return !0
            }
            /*!
            Event object based on jQuery events, MIT license

            https://jquery.org/license/
            https://tldrlegal.com/license/mit-license
            https://github.com/jquery/jquery/blob/master/src/event.js
            */
            var a = function (e, t) {
                return this instanceof a ? (e && e.type ? (this.originalEvent = e, this.type = e.type, this.isDefaultPrevented = e.defaultPrevented ? i : n) : this.type = e, t && (this.type = void 0 !== t.type ? t.type : this.type, this.cy = t.cy, this.cyTarget = t.cyTarget, this.cyPosition = t.cyPosition, this.cyRenderedPosition = t.cyRenderedPosition, this.namespace = t.namespace, this.layout = t.layout, this.data = t.data, this.message = t.message), void (this.timeStamp = e && e.timeStamp || Date.now())) : new a(e, t)
            };
            a.prototype = {
                instanceString: function () {
                    return "event"
                },
                preventDefault: function () {
                    this.isDefaultPrevented = i;
                    var e = this.originalEvent;
                    e && e.preventDefault && e.preventDefault()
                },
                stopPropagation: function () {
                    this.isPropagationStopped = i;
                    var e = this.originalEvent;
                    e && e.stopPropagation && e.stopPropagation()
                },
                stopImmediatePropagation: function () {
                    this.isImmediatePropagationStopped = i, this.stopPropagation()
                },
                isDefaultPrevented: n,
                isPropagationStopped: n,
                isImmediatePropagationStopped: n
            }, t.exports = a
        }, {}],
        46: [function (e, t, r) {
            "use strict";

            function n(e, t, r) {
                var n = r,
                    a = function (r) {
                        s.error("Can not register `" + t + "` for `" + e + "` since `" + r + "` already exists in the prototype and can not be overridden")
                    };
                if ("core" === e) {
                    if (c.prototype[t]) return a(t);
                    c.prototype[t] = r
                } else if ("collection" === e) {
                    if (u.prototype[t]) return a(t);
                    u.prototype[t] = r
                } else if ("layout" === e) {
                    for (var o = function (e) {
                        this.options = e, r.call(this, e), h.plainObject(this._private) || (this._private = {}), this._private.cy = e.cy, this._private.listeners = []
                    }, d = o.prototype = Object.create(r.prototype), f = [], v = 0; v < f.length; v++) {
                        var g = f[v];
                        d[g] = d[g] || function () {
                            return this
                        }
                    }
                    d.start && !d.run ? d.run = function () {
                        return this.start(), this
                    } : !d.start && d.run && (d.start = function () {
                        return this.run(), this
                    });
                    var y = r.prototype.stop;
                    d.stop = function () {
                        var e = this.options;
                        if (e && e.animate) {
                            var t = this.animations;
                            if (t)
                                for (var r = 0; r < t.length; r++) t[r].stop()
                        }
                        return y ? y.call(this) : this.trigger("layoutstop"), this
                    }, d.destroy || (d.destroy = function () {
                        return this
                    }), d.on = l.on({
                        layout: !0
                    }), d.one = l.on({
                        layout: !0,
                        unbindSelfOnTrigger: !0
                    }), d.once = l.on({
                        layout: !0,
                        unbindAllBindersOnTrigger: !0
                    }), d.off = l.off({
                        layout: !0
                    }), d.trigger = l.trigger({
                        layout: !0
                    }), l.eventAliasesOn(d), n = o
                } else if ("renderer" === e && "null" !== t && "base" !== t) {
                    var m = i("renderer", "base"),
                        b = m.prototype,
                        x = r,
                        w = r.prototype,
                        E = function () {
                            m.apply(this, arguments), x.apply(this, arguments)
                        },
                        _ = E.prototype;
                    for (var S in b) {
                        var P = b[S],
                            T = null != w[S];
                        if (T) return a(S);
                        _[S] = P
                    }
                    for (var S in w) _[S] = w[S];
                    b.clientFunctions.forEach(function (e) {
                        _[e] = _[e] || function () {
                            s.error("Renderer does not implement `renderer." + e + "()` on its prototype")
                        }
                    }), n = E
                }
                return s.setMap({
                    map: p,
                    keys: [e, t],
                    value: n
                })
            }

            function i(e, t) {
                return s.getMap({
                    map: p,
                    keys: [e, t]
                })
            }

            function a(e, t, r, n, i) {
                return s.setMap({
                    map: f,
                    keys: [e, t, r, n],
                    value: i
                })
            }

            function o(e, t, r, n) {
                return s.getMap({
                    map: f,
                    keys: [e, t, r, n]
                })
            }
            var s = e("./util"),
                l = e("./define"),
                u = e("./collection"),
                c = e("./core"),
                d = e("./extensions"),
                h = e("./is"),
                p = {},
                f = {},
                v = function () {
                    return 2 === arguments.length ? i.apply(null, arguments) : 3 === arguments.length ? n.apply(null, arguments) : 4 === arguments.length ? o.apply(null, arguments) : 5 === arguments.length ? a.apply(null, arguments) : void s.error("Invalid extension access syntax")
                };
            c.prototype.extension = v, d.forEach(function (e) {
                e.extensions.forEach(function (t) {
                    n(e.type, t.name, t.impl)
                })
            }), t.exports = v
        }, {
            "./collection": 26,
            "./core": 37,
            "./define": 44,
            "./extensions": 47,
            "./is": 83,
            "./util": 100
        }],
        47: [function (e, t, r) {
            "use strict";
            t.exports = [{
                type: "layout",
                extensions: e("./layout")
            }, {
                type: "renderer",
                extensions: e("./renderer")
            }]
        }, {
            "./layout": 53,
            "./renderer": 78
        }],
        48: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, s, e)
            }
            var i = e("../../util"),
                a = e("../../math"),
                o = e("../../is"),
                s = {
                    fit: !0,
                    directed: !1,
                    padding: 30,
                    circle: !1,
                    spacingFactor: 1.75,
                    boundingBox: void 0,
                    avoidOverlap: !0,
                    roots: void 0,
                    maximalAdjustments: 0,
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                var e, t = this.options,
                    r = t,
                    n = t.cy,
                    i = r.eles,
                    s = i.nodes().not(":parent"),
                    l = i,
                    u = a.makeBoundingBox(r.boundingBox ? r.boundingBox : {
                        x1: 0,
                        y1: 0,
                        w: n.width(),
                        h: n.height()
                    });
                if (o.elementOrCollection(r.roots)) e = r.roots;
                else if (o.array(r.roots)) {
                    for (var c = [], d = 0; d < r.roots.length; d++) {
                        var h = r.roots[d],
                            p = n.getElementById(h);
                        c.push(p)
                    }
                    e = n.collection(c)
                } else if (o.string(r.roots)) e = n.$(r.roots);
                else if (r.directed) e = s.roots();
                else {
                    for (var f = [], v = s; v.length > 0;) {
                        var g = n.collection();
                        i.bfs({
                            roots: v[0],
                            visit: function (e, t, r, n, i) {
                                g = g.add(r)
                            },
                            directed: !1
                        }), v = v.not(g), f.push(g)
                    }
                    e = n.collection();
                    for (var d = 0; d < f.length; d++) {
                        var y = f[d],
                            m = y.maxDegree(!1),
                            b = y.filter(function () {
                                return this.degree(!1) === m
                            });
                        e = e.add(b)
                    }
                }
                var x = [],
                    w = {},
                    E = {},
                    _ = {},
                    S = {},
                    P = {};
                l.bfs({
                    roots: e,
                    directed: r.directed,
                    visit: function (e, t, r, n, i) {
                        var a = this[0],
                            o = a.id();
                        if (x[t] || (x[t] = []), x[t].push(a), w[o] = !0, E[o] = t, _[o] = i, S[o] = n, i) {
                            var s = i.id(),
                                l = P[s] = P[s] || [];
                            l.push(r)
                        }
                    }
                });
                for (var T = [], d = 0; d < s.length; d++) {
                    var p = s[d];
                    w[p.id()] || T.push(p)
                }
                for (var k = 3 * T.length, D = 0; 0 !== T.length && k > D;) {
                    for (var C = T.shift(), M = C.neighborhood().nodes(), N = !1, d = 0; d < M.length; d++) {
                        var B = E[M[d].id()];
                        if (void 0 !== B) {
                            x[B].push(C), N = !0;
                            break
                        }
                    }
                    N || T.push(C), D++
                }
                for (; 0 !== T.length;) {
                    var C = T.shift(),
                        N = !1;
                    N || (0 === x.length && x.push([]), x[0].push(C))
                }
                var z = function () {
                    for (var e = 0; e < x.length; e++)
                        for (var t = x[e], r = 0; r < t.length; r++) {
                            var n = t[r];
                            n._private.scratch.breadthfirst = {
                                depth: e,
                                index: r
                            }
                        }
                };
                z();
                for (var I = function (e) {
                    for (var t, r = e.connectedEdges(function () {
                        return this.data("target") === e.id()
                    }), n = e._private.scratch.breadthfirst, i = 0, a = 0; a < r.length; a++) {
                        var o = r[a],
                            s = o.source()[0],
                            l = s._private.scratch.breadthfirst;
                        n.depth <= l.depth && i < l.depth && (i = l.depth, t = s)
                    }
                    return t
                }, L = 0; L < r.maximalAdjustments; L++) {
                    for (var O = x.length, A = [], d = 0; O > d; d++)
                        for (var B = x[d], R = B.length, q = 0; R > q; q++) {
                            var p = B[q],
                                V = p._private.scratch.breadthfirst,
                                F = I(p);
                            F && (V.intEle = F, A.push(p))
                        }
                    for (var d = 0; d < A.length; d++) {
                        var p = A[d],
                            V = p._private.scratch.breadthfirst,
                            F = V.intEle,
                            j = F._private.scratch.breadthfirst;
                        x[V.depth].splice(V.index, 1);
                        for (var X = j.depth + 1; X > x.length - 1;) x.push([]);
                        x[X].push(p), V.depth = X, V.index = x[X].length - 1
                    }
                    z()
                }
                var Y = 0;
                if (r.avoidOverlap) {
                    for (var d = 0; d < s.length; d++) {
                        var W = s[d],
                            $ = W.boundingBox(),
                            H = $.w,
                            U = $.h;
                        Y = Math.max(Y, H, U)
                    }
                    Y *= r.spacingFactor
                }
                for (var Z = {}, G = function (e) {
                    if (Z[e.id()]) return Z[e.id()];
                    for (var t = e._private.scratch.breadthfirst.depth, r = e.neighborhood().nodes().not(":parent").intersection(s), n = 0, i = 0, a = 0; a < r.length; a++) {
                        var o = r[a],
                            l = o._private.scratch.breadthfirst,
                            u = l.index,
                            c = l.depth,
                            d = x[c].length;
                        (t > c || 0 === t) && (n += u / d, i++)
                    }
                    return i = Math.max(1, i), n /= i, 0 === i && (n = void 0), Z[e.id()] = n, n
                }, Q = function (e, t) {
                    var r = G(e),
                        n = G(t);
                    return r - n
                }, K = 0; 3 > K; K++) {
                    for (var d = 0; d < x.length; d++) x[d] = x[d].sort(Q);
                    z()
                }
                for (var J = 0, d = 0; d < x.length; d++) J = Math.max(x[d].length, J);
                for (var ee = {
                    x: u.x1 + u.w / 2,
                    y: u.x1 + u.h / 2
                }, te = function (e, t) {
                    var n = e._private.scratch.breadthfirst,
                        i = n.depth,
                        a = n.index,
                        o = x[i].length,
                        s = Math.max(u.w / (o + 1), Y),
                        l = Math.max(u.h / (x.length + 1), Y),
                        c = Math.min(u.w / 2 / x.length, u.h / 2 / x.length);
                    if (c = Math.max(c, Y), r.circle) {
                        if (r.circle) {
                            var d = c * i + c - (x.length > 0 && x[0].length <= 3 ? c / 2 : 0),
                                h = 2 * Math.PI / x[i].length * a;
                            return 0 === i && 1 === x[0].length && (d = 1), {
                                x: ee.x + d * Math.cos(h),
                                y: ee.y + d * Math.sin(h)
                            }
                        }
                        return {
                            x: ee.x + (a + 1 - (o + 1) / 2) * s,
                            y: (i + 1) * l
                        }
                    }
                    var p = {
                        x: ee.x + (a + 1 - (o + 1) / 2) * s,
                        y: (i + 1) * l
                    };
                    return t ? p : p
                }, re = {}, d = x.length - 1; d >= 0; d--)
                    for (var B = x[d], q = 0; q < B.length; q++) {
                        var C = B[q];
                        re[C.id()] = te(C, d === x.length - 1)
                    }
                return s.layoutPositions(this, r, function () {
                    return re[this.id()]
                }), this
            }, t.exports = n
        }, {
            "../../is": 83,
            "../../math": 85,
            "../../util": 100
        }],
        49: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, s, e)
            }
            var i = e("../../util"),
                a = e("../../math"),
                o = e("../../is"),
                s = {
                    fit: !0,
                    padding: 30,
                    boundingBox: void 0,
                    avoidOverlap: !0,
                    radius: void 0,
                    startAngle: 1.5 * Math.PI,
                    sweep: void 0,
                    clockwise: !0,
                    sort: void 0,
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                var e = this.options,
                    t = e,
                    r = e.cy,
                    n = t.eles,
                    i = void 0 !== t.counterclockwise ? !t.counterclockwise : t.clockwise,
                    s = n.nodes().not(":parent");
                t.sort && (s = s.sort(t.sort));
                for (var l, u = a.makeBoundingBox(t.boundingBox ? t.boundingBox : {
                    x1: 0,
                    y1: 0,
                    w: r.width(),
                    h: r.height()
                }), c = {
                    x: u.x1 + u.w / 2,
                    y: u.y1 + u.h / 2
                }, d = void 0 === t.sweep ? 2 * Math.PI - 2 * Math.PI / s.length : t.sweep, h = d / Math.max(1, s.length - 1), p = 0, f = 0; f < s.length; f++) {
                    var v = s[f],
                        g = v.boundingBox(),
                        y = g.w,
                        m = g.h;
                    p = Math.max(p, y, m)
                }
                if (l = o.number(t.radius) ? t.radius : s.length <= 1 ? 0 : Math.min(u.h, u.w) / 2 - p, s.length > 1 && t.avoidOverlap) {
                    p *= 1.75;
                    var b = Math.cos(h) - Math.cos(0),
                        x = Math.sin(h) - Math.sin(0),
                        w = Math.sqrt(p * p / (b * b + x * x));
                    l = Math.max(w, l)
                }
                var E = function (e, r) {
                    var n = t.startAngle + e * h * (i ? 1 : -1),
                        a = l * Math.cos(n),
                        o = l * Math.sin(n),
                        s = {
                            x: c.x + a,
                            y: c.y + o
                        };
                    return s
                };
                return s.layoutPositions(this, t, E), this
            }, t.exports = n
        }, {
            "../../is": 83,
            "../../math": 85,
            "../../util": 100
        }],
        50: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, o, e)
            }
            var i = e("../../util"),
                a = e("../../math"),
                o = {
                    fit: !0,
                    padding: 30,
                    startAngle: 1.5 * Math.PI,
                    sweep: void 0,
                    clockwise: !0,
                    equidistant: !1,
                    minNodeSpacing: 10,
                    boundingBox: void 0,
                    avoidOverlap: !0,
                    height: void 0,
                    width: void 0,
                    concentric: function (e) {
                        return e.degree()
                    },
                    levelWidth: function (e) {
                        return e.maxDegree() / 4
                    },
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                for (var e = this.options, t = e, r = void 0 !== t.counterclockwise ? !t.counterclockwise : t.clockwise, n = e.cy, i = t.eles, o = i.nodes().not(":parent"), s = a.makeBoundingBox(t.boundingBox ? t.boundingBox : {
                    x1: 0,
                    y1: 0,
                    w: n.width(),
                    h: n.height()
                }), l = {
                    x: s.x1 + s.w / 2,
                    y: s.y1 + s.h / 2
                }, u = [], c = t.startAngle, d = 0, h = 0; h < o.length; h++) {
                    var p, f = o[h];
                    p = t.concentric.apply(f, [f]), u.push({
                        value: p,
                        node: f
                    }), f._private.scratch.concentric = p
                }
                o.updateStyle();
                for (var h = 0; h < o.length; h++) {
                    var f = o[h],
                        v = f.boundingBox();
                    d = Math.max(d, v.w, v.h)
                }
                u.sort(function (e, t) {
                    return t.value - e.value
                });
                for (var g = t.levelWidth(o), y = [
                    []
                ], m = y[0], h = 0; h < u.length; h++) {
                    var b = u[h];
                    if (m.length > 0) {
                        var x = Math.abs(m[0].value - b.value);
                        x >= g && (m = [], y.push(m))
                    }
                    m.push(b)
                }
                var w = d + t.minNodeSpacing;
                if (!t.avoidOverlap) {
                    var E = y.length > 0 && y[0].length > 1,
                        _ = Math.min(s.w, s.h) / 2 - w,
                        S = _ / (y.length + E ? 1 : 0);
                    w = Math.min(w, S)
                }
                for (var P = 0, h = 0; h < y.length; h++) {
                    var T = y[h],
                        k = void 0 === t.sweep ? 2 * Math.PI - 2 * Math.PI / T.length : t.sweep,
                        D = T.dTheta = k / Math.max(1, T.length - 1);
                    if (T.length > 1 && t.avoidOverlap) {
                        var C = Math.cos(D) - Math.cos(0),
                            M = Math.sin(D) - Math.sin(0),
                            N = Math.sqrt(w * w / (C * C + M * M));
                        P = Math.max(N, P)
                    }
                    T.r = P, P += w
                }
                if (t.equidistant) {
                    for (var B = 0, P = 0, h = 0; h < y.length; h++) {
                        var T = y[h],
                            z = T.r - P;
                        B = Math.max(B, z)
                    }
                    P = 0;
                    for (var h = 0; h < y.length; h++) {
                        var T = y[h];
                        0 === h && (P = T.r), T.r = P, P += B
                    }
                }
                for (var I = {}, h = 0; h < y.length; h++)
                    for (var T = y[h], D = T.dTheta, P = T.r, L = 0; L < T.length; L++) {
                        var b = T[L],
                            c = t.startAngle + (r ? 1 : -1) * D * L,
                            O = {
                                x: l.x + P * Math.cos(c),
                                y: l.y + P * Math.sin(c)
                            };
                        I[b.node.id()] = O
                    }
                return o.layoutPositions(this, t, function () {
                    var e = this.id();
                    return I[e]
                }), this
            }, t.exports = n
        }, {
            "../../math": 85,
            "../../util": 100
        }],
        51: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = a.extend({}, u, e), this.options.layout = this
            }
            var i, a = e("../../util"),
                o = e("../../math"),
                s = e("../../thread"),
                l = e("../../is"),
                u = {
                    ready: function () { },
                    stop: function () { },
                    animate: !0,
                    animationThreshold: 250,
                    refresh: 20,
                    fit: !0,
                    padding: 30,
                    boundingBox: void 0,
                    randomize: !1,
                    componentSpacing: 100,
                    nodeRepulsion: function (e) {
                        return 4e5
                    },
                    nodeOverlap: 10,
                    idealEdgeLength: function (e) {
                        return 10
                    },
                    edgeElasticity: function (e) {
                        return 100
                    },
                    nestingFactor: 5,
                    gravity: 80,
                    numIter: 1e3,
                    initialTemp: 200,
                    coolingFactor: .95,
                    minTemp: 1,
                    useMultitasking: !0
                };
            n.prototype.run = function () {
                var e = this.options,
                    t = e.cy,
                    r = this,
                    n = this.thread;
                n && !n.stopped() || (n = this.thread = s({
                    disabled: !e.useMultitasking
                })), r.stopped = !1, r.trigger({
                    type: "layoutstart",
                    layout: r
                }), i = !0 === e.debug;
                var o = c(t, r, e);
                i && p(o), e.randomize && f(o, t);
                var l = Date.now(),
                    u = !1,
                    d = function (r) {
                        r = r || {}, u && !r.next || !r.force && Date.now() - l < e.animationThreshold || (u = !0, a.requestAnimationFrame(function () {
                            v(o, t, e), !0 === e.fit && t.fit(e.padding), u = !1, r.next && r.next()
                        }))
                    };
                n.on("message", function (e) {
                    var t = e.message;
                    o.layoutNodes = t, d()
                }), n.pass({
                    layoutInfo: o,
                    options: {
                        animate: e.animate,
                        refresh: e.refresh,
                        componentSpacing: e.componentSpacing,
                        nodeOverlap: e.nodeOverlap,
                        nestingFactor: e.nestingFactor,
                        gravity: e.gravity,
                        numIter: e.numIter,
                        initialTemp: e.initialTemp,
                        coolingFactor: e.coolingFactor,
                        minTemp: e.minTemp
                    }
                }).run(function (e) {
                    var t, r = e.layoutInfo,
                        n = e.options,
                        i = !1,
                        a = function (e, t, r) {
                            o(e, t), c(e, t), d(e, t), h(e, t), p(e, t)
                        },
                        o = function (e, t) {
                            for (var r = 0; r < e.graphSet.length; r++)
                                for (var n = e.graphSet[r], i = n.length, a = 0; i > a; a++)
                                    for (var o = e.layoutNodes[e.idToIndex[n[a]]], l = a + 1; i > l; l++) {
                                        var u = e.layoutNodes[e.idToIndex[n[l]]];
                                        s(o, u, e, t)
                                    }
                        },
                        s = function (e, t, r, n) {
                            var i = e.cmptId,
                                a = t.cmptId;
                            if (i === a || r.isCompound) {
                                var o = t.positionX - e.positionX,
                                    s = t.positionY - e.positionY;
                                if (0 !== o || 0 !== s) {
                                    var c = l(e, t, o, s);
                                    if (c > 0) var d = n.nodeOverlap * c,
                                        h = Math.sqrt(o * o + s * s),
                                        p = d * o / h,
                                        f = d * s / h;
                                    else var v = u(e, o, s),
                                        g = u(t, -1 * o, -1 * s),
                                        y = g.x - v.x,
                                        m = g.y - v.y,
                                        b = y * y + m * m,
                                        h = Math.sqrt(b),
                                        d = (e.nodeRepulsion + t.nodeRepulsion) / b,
                                        p = d * y / h,
                                        f = d * m / h;
                                    e.isLocked || (e.offsetX -= p, e.offsetY -= f), t.isLocked || (t.offsetX += p, t.offsetY += f)
                                }
                            }
                        },
                        l = function (e, t, r, n) {
                            if (r > 0) var i = e.maxX - t.minX;
                            else var i = t.maxX - e.minX;
                            if (n > 0) var a = e.maxY - t.minY;
                            else var a = t.maxY - e.minY;
                            return i >= 0 && a >= 0 ? Math.sqrt(i * i + a * a) : 0
                        },
                        u = function (e, t, r) {
                            var n = e.positionX,
                                i = e.positionY,
                                a = e.height || 1,
                                o = e.width || 1,
                                s = r / t,
                                l = a / o,
                                u = {};
                            return 0 === t && r > 0 ? (u.x = n, u.y = i + a / 2, u) : 0 === t && 0 > r ? (u.x = n, u.y = i + a / 2, u) : t > 0 && s >= -1 * l && l >= s ? (u.x = n + o / 2, u.y = i + o * r / 2 / t, u) : 0 > t && s >= -1 * l && l >= s ? (u.x = n - o / 2, u.y = i - o * r / 2 / t, u) : r > 0 && (-1 * l >= s || s >= l) ? (u.x = n + a * t / 2 / r, u.y = i + a / 2, u) : 0 > r && (-1 * l >= s || s >= l) ? (u.x = n - a * t / 2 / r, u.y = i - a / 2, u) : u
                        },
                        c = function (e, t) {
                            for (var r = 0; r < e.edgeSize; r++) {
                                var n = e.layoutEdges[r],
                                    i = e.idToIndex[n.sourceId],
                                    a = e.layoutNodes[i],
                                    o = e.idToIndex[n.targetId],
                                    s = e.layoutNodes[o],
                                    l = s.positionX - a.positionX,
                                    c = s.positionY - a.positionY;
                                if (0 === l && 0 === c) return;
                                var d = u(a, l, c),
                                    h = u(s, -1 * l, -1 * c),
                                    p = h.x - d.x,
                                    f = h.y - d.y,
                                    v = Math.sqrt(p * p + f * f),
                                    g = Math.pow(n.idealLength - v, 2) / n.elasticity;
                                if (0 !== v) var y = g * p / v,
                                    m = g * f / v;
                                else var y = 0,
                                    m = 0;
                                a.isLocked || (a.offsetX += y, a.offsetY += m), s.isLocked || (s.offsetX -= y, s.offsetY -= m)
                            }
                        },
                        d = function (e, t) {
                            for (var r = 1, n = 0; n < e.graphSet.length; n++) {
                                var i = e.graphSet[n],
                                    a = i.length;
                                if (0 === n) var o = e.clientHeight / 2,
                                    s = e.clientWidth / 2;
                                else var l = e.layoutNodes[e.idToIndex[i[0]]],
                                    u = e.layoutNodes[e.idToIndex[l.parentId]],
                                    o = u.positionX,
                                    s = u.positionY;
                                for (var c = 0; a > c; c++) {
                                    var d = e.layoutNodes[e.idToIndex[i[c]]];
                                    if (!d.isLocked) {
                                        var h = o - d.positionX,
                                            p = s - d.positionY,
                                            f = Math.sqrt(h * h + p * p);
                                        if (f > r) {
                                            var v = t.gravity * h / f,
                                                g = t.gravity * p / f;
                                            d.offsetX += v, d.offsetY += g
                                        }
                                    }
                                }
                            }
                        },
                        h = function (e, t) {
                            var r = [],
                                n = 0,
                                i = -1;
                            for (r.push.apply(r, e.graphSet[0]), i += e.graphSet[0].length; i >= n;) {
                                var a = r[n++],
                                    o = e.idToIndex[a],
                                    s = e.layoutNodes[o],
                                    l = s.children;
                                if (0 < l.length && !s.isLocked) {
                                    for (var u = s.offsetX, c = s.offsetY, d = 0; d < l.length; d++) {
                                        var h = e.layoutNodes[e.idToIndex[l[d]]];
                                        h.offsetX += u, h.offsetY += c, r[++i] = l[d]
                                    }
                                    s.offsetX = 0, s.offsetY = 0
                                }
                            }
                        },
                        p = function (e, t) {
                            for (var r = 0; r < e.nodeSize; r++) {
                                var n = e.layoutNodes[r];
                                0 < n.children.length && (n.maxX = void 0, n.minX = void 0, n.maxY = void 0, n.minY = void 0)
                            }
                            for (var r = 0; r < e.nodeSize; r++) {
                                var n = e.layoutNodes[r];
                                if (!(0 < n.children.length || n.isLocked)) {
                                    var i = f(n.offsetX, n.offsetY, e.temperature);
                                    n.positionX += i.x, n.positionY += i.y, n.offsetX = 0, n.offsetY = 0, n.minX = n.positionX - n.width, n.maxX = n.positionX + n.width, n.minY = n.positionY - n.height, n.maxY = n.positionY + n.height, v(n, e)
                                }
                            }
                            for (var r = 0; r < e.nodeSize; r++) {
                                var n = e.layoutNodes[r];
                                0 < n.children.length && !n.isLocked && (n.positionX = (n.maxX + n.minX) / 2, n.positionY = (n.maxY + n.minY) / 2, n.width = n.maxX - n.minX, n.height = n.maxY - n.minY)
                            }
                        },
                        f = function (e, t, r) {
                            var n = Math.sqrt(e * e + t * t);
                            if (n > r) var i = {
                                x: r * e / n,
                                y: r * t / n
                            };
                            else var i = {
                                x: e,
                                y: t
                            };
                            return i
                        },
                        v = function (e, t) {
                            var r = e.parentId;
                            if (null != r) {
                                var n = t.layoutNodes[t.idToIndex[r]],
                                    i = !1;
                                return (null == n.maxX || e.maxX + n.padRight > n.maxX) && (n.maxX = e.maxX + n.padRight, i = !0), (null == n.minX || e.minX - n.padLeft < n.minX) && (n.minX = e.minX - n.padLeft, i = !0), (null == n.maxY || e.maxY + n.padBottom > n.maxY) && (n.maxY = e.maxY + n.padBottom, i = !0), (null == n.minY || e.minY - n.padTop < n.minY) && (n.minY = e.minY - n.padTop, i = !0), i ? v(n, t) : void 0
                            }
                        },
                        g = function (e, t) {
                            for (var n = r.layoutNodes, i = [], a = 0; a < n.length; a++) {
                                var o = n[a],
                                    s = o.cmptId,
                                    l = i[s] = i[s] || [];
                                l.push(o)
                            }
                            for (var u = 0, a = 0; a < i.length; a++) {
                                var c = i[a];
                                if (c) {
                                    c.x1 = 1 / 0, c.x2 = -(1 / 0), c.y1 = 1 / 0, c.y2 = -(1 / 0);
                                    for (var d = 0; d < c.length; d++) {
                                        var h = c[d];
                                        c.x1 = Math.min(c.x1, h.positionX - h.width / 2), c.x2 = Math.max(c.x2, h.positionX + h.width / 2), c.y1 = Math.min(c.y1, h.positionY - h.height / 2), c.y2 = Math.max(c.y2, h.positionY + h.height / 2)
                                    }
                                    c.w = c.x2 - c.x1, c.h = c.y2 - c.y1, u += c.w * c.h
                                }
                            }
                            i.sort(function (e, t) {
                                return t.w * t.h - e.w * e.h
                            });
                            for (var p = 0, f = 0, v = 0, g = 0, y = Math.sqrt(u) * r.clientWidth / r.clientHeight, a = 0; a < i.length; a++) {
                                var c = i[a];
                                if (c) {
                                    for (var d = 0; d < c.length; d++) {
                                        var h = c[d];
                                        h.isLocked || (h.positionX += p, h.positionY += f)
                                    }
                                    p += c.w + t.componentSpacing, v += c.w + t.componentSpacing, g = Math.max(g, c.h), v > y && (f += g + t.componentSpacing, p = 0, v = 0, g = 0)
                                }
                            }
                        },
                        y = function (e) {
                            return i ? !1 : (a(r, n, e), r.temperature = r.temperature * n.coolingFactor, !(r.temperature < n.minTemp))
                        },
                        m = 0;
                    do {
                        for (var b = 0; b < n.refresh && m < n.numIter;) {
                            var t = y(m);
                            if (!t) break;
                            b++ , m++
                        }
                        n.animate && broadcast(r.layoutNodes)
                    } while (t && m + 1 < n.numIter);
                    return g(r, n), r
                }).then(function (e) {
                    o.layoutNodes = e.layoutNodes, n.stop(), h()
                });
                var h = function () {
                    d({
                        force: !0,
                        next: function () {
                            r.one("layoutstop", e.stop), r.trigger({
                                type: "layoutstop",
                                layout: r
                            })
                        }
                    })
                };
                return this
            }, n.prototype.stop = function () {
                return this.stopped = !0, this.thread && this.thread.stop(), this.trigger("layoutstop"), this
            }, n.prototype.destroy = function () {
                return this.thread && this.thread.stop(), this
            };
            var c = function (e, t, r) {
                for (var n = r.eles.edges(), i = r.eles.nodes(), a = {
                    isCompound: e.hasCompoundNodes(),
                    layoutNodes: [],
                    idToIndex: {},
                    nodeSize: i.size(),
                    graphSet: [],
                    indexToGraph: [],
                    layoutEdges: [],
                    edgeSize: n.size(),
                    temperature: r.initialTemp,
                    clientWidth: e.width(),
                    clientHeight: e.width(),
                    boundingBox: o.makeBoundingBox(r.boundingBox ? r.boundingBox : {
                        x1: 0,
                        y1: 0,
                        w: e.width(),
                        h: e.height()
                    })
                }, s = r.eles.components(), u = {}, c = 0; c < s.length; c++)
                    for (var h = s[c], p = 0; p < h.length; p++) {
                        var f = h[p];
                        u[f.id()] = c
                    }
                for (var c = 0; c < a.nodeSize; c++) {
                    var v = i[c],
                        g = v.boundingBox(),
                        y = {};
                    y.isLocked = v.locked(), y.id = v.data("id"), y.parentId = v.data("parent"), y.cmptId = u[v.id()], y.children = [], y.positionX = v.position("x"), y.positionY = v.position("y"), y.offsetX = 0, y.offsetY = 0, y.height = g.w, y.width = g.h, y.maxX = y.positionX + y.width / 2, y.minX = y.positionX - y.width / 2, y.maxY = y.positionY + y.height / 2, y.minY = y.positionY - y.height / 2, y.padLeft = parseFloat(v.style("padding")), y.padRight = parseFloat(v.style("padding")), y.padTop = parseFloat(v.style("padding")), y.padBottom = parseFloat(v.style("padding")), y.nodeRepulsion = l.fn(r.nodeRepulsion) ? r.nodeRepulsion.call(v, v) : r.nodeRepulsion, a.layoutNodes.push(y), a.idToIndex[y.id] = c
                }
                for (var m = [], b = 0, x = -1, w = [], c = 0; c < a.nodeSize; c++) {
                    var v = a.layoutNodes[c],
                        E = v.parentId;
                    null != E ? a.layoutNodes[a.idToIndex[E]].children.push(v.id) : (m[++x] = v.id, w.push(v.id))
                }
                for (a.graphSet.push(w); x >= b;) {
                    var _ = m[b++],
                        S = a.idToIndex[_],
                        f = a.layoutNodes[S],
                        P = f.children;
                    if (P.length > 0) {
                        a.graphSet.push(P);
                        for (var c = 0; c < P.length; c++) m[++x] = P[c]
                    }
                }
                for (var c = 0; c < a.graphSet.length; c++)
                    for (var T = a.graphSet[c], p = 0; p < T.length; p++) {
                        var k = a.idToIndex[T[p]];
                        a.indexToGraph[k] = c
                    }
                for (var c = 0; c < a.edgeSize; c++) {
                    var D = n[c],
                        C = {};
                    C.id = D.data("id"), C.sourceId = D.data("source"), C.targetId = D.data("target");
                    var M = l.fn(r.idealEdgeLength) ? r.idealEdgeLength.call(D, D) : r.idealEdgeLength,
                        N = l.fn(r.edgeElasticity) ? r.edgeElasticity.call(D, D) : r.edgeElasticity,
                        B = a.idToIndex[C.sourceId],
                        z = a.idToIndex[C.targetId],
                        I = a.indexToGraph[B],
                        L = a.indexToGraph[z];
                    if (I != L) {
                        for (var O = d(C.sourceId, C.targetId, a), A = a.graphSet[O], R = 0, y = a.layoutNodes[B]; - 1 === A.indexOf(y.id);) y = a.layoutNodes[a.idToIndex[y.parentId]], R++;
                        for (y = a.layoutNodes[z]; - 1 === A.indexOf(y.id);) y = a.layoutNodes[a.idToIndex[y.parentId]], R++;
                        M *= R * r.nestingFactor
                    }
                    C.idealLength = M, C.elasticity = N, a.layoutEdges.push(C)
                }
                return a
            },
                d = function (e, t, r) {
                    var n = h(e, t, 0, r);
                    return 2 > n.count ? 0 : n.graph
                },
                h = function (e, t, r, n) {
                    var i = n.graphSet[r];
                    if (-1 < i.indexOf(e) && -1 < i.indexOf(t)) return {
                        count: 2,
                        graph: r
                    };
                    for (var a = 0, o = 0; o < i.length; o++) {
                        var s = i[o],
                            l = n.idToIndex[s],
                            u = n.layoutNodes[l].children;
                        if (0 !== u.length) {
                            var c = n.indexToGraph[n.idToIndex[u[0]]],
                                d = h(e, t, c, n);
                            if (0 !== d.count) {
                                if (1 !== d.count) return d;
                                if (a++ , 2 === a) break
                            }
                        }
                    }
                    return {
                        count: a,
                        graph: r
                    }
                },
                p = function (e) {
                    if (i) {
                        console.debug("layoutNodes:");
                        for (var t = 0; t < e.nodeSize; t++) {
                            var r = e.layoutNodes[t],
                                n = "\nindex: " + t + "\nId: " + r.id + "\nChildren: " + r.children.toString() + "\nparentId: " + r.parentId + "\npositionX: " + r.positionX + "\npositionY: " + r.positionY + "\nOffsetX: " + r.offsetX + "\nOffsetY: " + r.offsetY + "\npadLeft: " + r.padLeft + "\npadRight: " + r.padRight + "\npadTop: " + r.padTop + "\npadBottom: " + r.padBottom;
                            console.debug(n)
                        }
                        console.debug("idToIndex");
                        for (var t in e.idToIndex) console.debug("Id: " + t + "\nIndex: " + e.idToIndex[t]);
                        console.debug("Graph Set");
                        for (var a = e.graphSet, t = 0; t < a.length; t++) console.debug("Set : " + t + ": " + a[t].toString());
                        for (var n = "IndexToGraph", t = 0; t < e.indexToGraph.length; t++) n += "\nIndex : " + t + " Graph: " + e.indexToGraph[t];
                        console.debug(n), n = "Layout Edges";
                        for (var t = 0; t < e.layoutEdges.length; t++) {
                            var o = e.layoutEdges[t];
                            n += "\nEdge Index: " + t + " ID: " + o.id + " SouceID: " + o.sourceId + " TargetId: " + o.targetId + " Ideal Length: " + o.idealLength
                        }
                        console.debug(n), n = "nodeSize: " + e.nodeSize, n += "\nedgeSize: " + e.edgeSize, n += "\ntemperature: " + e.temperature, console.debug(n)
                    }
                },
                f = function (e, t) {
                    for (var r = e.clientWidth, n = e.clientHeight, i = 0; i < e.nodeSize; i++) {
                        var a = e.layoutNodes[i];
                        0 !== a.children.length || a.isLocked || (a.positionX = Math.random() * r, a.positionY = Math.random() * n)
                    }
                },
                v = function (e, t, r) {
                    var n = r.layout,
                        i = r.eles.nodes(),
                        a = e.boundingBox,
                        o = {
                            x1: 1 / 0,
                            x2: -(1 / 0),
                            y1: 1 / 0,
                            y2: -(1 / 0)
                        };
                    r.boundingBox && (i.forEach(function (t) {
                        var r = e.layoutNodes[e.idToIndex[t.data("id")]];
                        o.x1 = Math.min(o.x1, r.positionX), o.x2 = Math.max(o.x2, r.positionX), o.y1 = Math.min(o.y1, r.positionY), o.y2 = Math.max(o.y2, r.positionY)
                    }), o.w = o.x2 - o.x1, o.h = o.y2 - o.y1), i.positions(function (t, n) {
                        var i = e.layoutNodes[e.idToIndex[n.data("id")]];
                        if (r.boundingBox) {
                            var s = (i.positionX - o.x1) / o.w,
                                l = (i.positionY - o.y1) / o.h;
                            return {
                                x: a.x1 + s * a.w,
                                y: a.y1 + l * a.h
                            }
                        }
                        return {
                            x: i.positionX,
                            y: i.positionY
                        }
                    }), !0 !== e.ready && (e.ready = !0, n.one("layoutready", r.ready), n.trigger({
                        type: "layoutready",
                        layout: this
                    }))
                };
            t.exports = n
        }, {
            "../../is": 83,
            "../../math": 85,
            "../../thread": 98,
            "../../util": 100
        }],
        52: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, o, e)
            }
            var i = e("../../util"),
                a = e("../../math"),
                o = {
                    fit: !0,
                    padding: 30,
                    boundingBox: void 0,
                    avoidOverlap: !0,
                    avoidOverlapPadding: 10,
                    condense: !1,
                    rows: void 0,
                    cols: void 0,
                    position: function (e) { },
                    sort: void 0,
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                var e = this.options,
                    t = e,
                    r = e.cy,
                    n = t.eles,
                    i = n.nodes().not(":parent");
                t.sort && (i = i.sort(t.sort));
                var o = a.makeBoundingBox(t.boundingBox ? t.boundingBox : {
                    x1: 0,
                    y1: 0,
                    w: r.width(),
                    h: r.height()
                });
                if (0 === o.h || 0 === o.w) i.layoutPositions(this, t, function () {
                    return {
                        x: o.x1,
                        y: o.y1
                    }
                });
                else {
                    var s = i.size(),
                        l = Math.sqrt(s * o.h / o.w),
                        u = Math.round(l),
                        c = Math.round(o.w / o.h * l),
                        d = function (e) {
                            if (null == e) return Math.min(u, c);
                            var t = Math.min(u, c);
                            t == u ? u = e : c = e
                        },
                        h = function (e) {
                            if (null == e) return Math.max(u, c);
                            var t = Math.max(u, c);
                            t == u ? u = e : c = e
                        },
                        p = t.rows,
                        f = null != t.cols ? t.cols : t.columns;
                    if (null != p && null != f) u = p, c = f;
                    else if (null != p && null == f) u = p, c = Math.ceil(s / u);
                    else if (null == p && null != f) c = f, u = Math.ceil(s / c);
                    else if (c * u > s) {
                        var v = d(),
                            g = h();
                        (v - 1) * g >= s ? d(v - 1) : (g - 1) * v >= s && h(g - 1)
                    } else
                        for (; s > c * u;) {
                            var v = d(),
                                g = h();
                            (g + 1) * v >= s ? h(g + 1) : d(v + 1)
                        }
                    var y = o.w / c,
                        m = o.h / u;
                    if (t.condense && (y = 0, m = 0), t.avoidOverlap)
                        for (var b = 0; b < i.length; b++) {
                            var x = i[b],
                                w = x._private.position;
                            null != w.x && null != w.y || (w.x = 0, w.y = 0);
                            var E = x.boundingBox(),
                                _ = t.avoidOverlapPadding,
                                S = E.w + _,
                                P = E.h + _;
                            y = Math.max(y, S), m = Math.max(m, P)
                        }
                    for (var T = {}, k = function (e, t) {
                        return !!T["c-" + e + "-" + t]
                    }, D = function (e, t) {
                        T["c-" + e + "-" + t] = !0
                    }, C = 0, M = 0, N = function () {
                        M++ , M >= c && (M = 0, C++)
                    }, B = {}, b = 0; b < i.length; b++) {
                        var x = i[b],
                            z = t.position(x);
                        if (z && (void 0 !== z.row || void 0 !== z.col)) {
                            var w = {
                                row: z.row,
                                col: z.col
                            };
                            if (void 0 === w.col)
                                for (w.col = 0; k(w.row, w.col);) w.col++;
                            else if (void 0 === w.row)
                                for (w.row = 0; k(w.row, w.col);) w.row++;
                            B[x.id()] = w, D(w.row, w.col)
                        }
                    }
                    var I = function (e, t) {
                        var r, n;
                        if (t.locked() || t.isParent()) return !1;
                        var i = B[t.id()];
                        if (i) r = i.col * y + y / 2 + o.x1, n = i.row * m + m / 2 + o.y1;
                        else {
                            for (; k(C, M);) N();
                            r = M * y + y / 2 + o.x1, n = C * m + m / 2 + o.y1, D(C, M), N()
                        }
                        return {
                            x: r,
                            y: n
                        }
                    };
                    i.layoutPositions(this, t, I)
                }
                return this
            }, t.exports = n
        }, {
            "../../math": 85,
            "../../util": 100
        }],
        53: [function (e, t, r) {
            "use strict";
            t.exports = [{
                name: "breadthfirst",
                impl: e("./breadthfirst")
            }, {
                name: "circle",
                impl: e("./circle")
            }, {
                name: "concentric",
                impl: e("./concentric")
            }, {
                name: "cose",
                impl: e("./cose")
            }, {
                name: "grid",
                impl: e("./grid")
            }, {
                name: "null",
                impl: e("./null")
            }, {
                name: "preset",
                impl: e("./preset")
            }, {
                name: "random",
                impl: e("./random")
            }]
        }, {
            "./breadthfirst": 48,
            "./circle": 49,
            "./concentric": 50,
            "./cose": 51,
            "./grid": 52,
            "./null": 54,
            "./preset": 55,
            "./random": 56
        }],
        54: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, a, e)
            }
            var i = e("../../util"),
                a = {
                    ready: function () { },
                    stop: function () { }
                };
            n.prototype.run = function () {
                var e = this.options,
                    t = e.eles,
                    r = this;
                e.cy;
                return r.trigger("layoutstart"), t.nodes().positions(function () {
                    return {
                        x: 0,
                        y: 0
                    }
                }), r.one("layoutready", e.ready), r.trigger("layoutready"), r.one("layoutstop", e.stop), r.trigger("layoutstop"), this
            }, n.prototype.stop = function () {
                return this
            }, t.exports = n
        }, {
            "../../util": 100
        }],
        55: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, o, e)
            }
            var i = e("../../util"),
                a = e("../../is"),
                o = {
                    positions: void 0,
                    zoom: void 0,
                    pan: void 0,
                    fit: !0,
                    padding: 30,
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                function e(e) {
                    if (null == t.positions) return null;
                    if (i) return t.positions.apply(e, [e]);
                    var r = t.positions[e._private.data.id];
                    return null == r ? null : r
                }
                var t = this.options,
                    r = t.eles,
                    n = r.nodes(),
                    i = a.fn(t.positions);
                return n.layoutPositions(this, t, function (t, r) {
                    var n = e(r);
                    return r.locked() || null == n ? !1 : n
                }), this
            }, t.exports = n
        }, {
            "../../is": 83,
            "../../util": 100
        }],
        56: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = i.extend({}, o, e)
            }
            var i = e("../../util"),
                a = e("../../math"),
                o = {
                    fit: !0,
                    padding: 30,
                    boundingBox: void 0,
                    animate: !1,
                    animationDuration: 500,
                    animationEasing: void 0,
                    ready: void 0,
                    stop: void 0
                };
            n.prototype.run = function () {
                var e = this.options,
                    t = e.cy,
                    r = e.eles,
                    n = r.nodes().not(":parent"),
                    i = a.makeBoundingBox(e.boundingBox ? e.boundingBox : {
                        x1: 0,
                        y1: 0,
                        w: t.width(),
                        h: t.height()
                    }),
                    o = function (e, t) {
                        return {
                            x: i.x1 + Math.round(Math.random() * i.w),
                            y: i.y1 + Math.round(Math.random() * i.h)
                        }
                    };
                return n.layoutPositions(this, e, o), this
            }, t.exports = n
        }, {
            "../../math": 85,
            "../../util": 100
        }],
        57: [function (e, t, r) {
            "use strict";
            var n = e("../../../math"),
                i = e("../../../is"),
                a = e("../../../util"),
                o = {};
            o.arrowShapeWidth = .3, o.registerArrowShapes = function () {
                var e = this.arrowShapes = {},
                    t = this,
                    r = function (e, t, r, n, i, a) {
                        var o = i.x - r / 2 - a,
                            s = i.x + r / 2 + a,
                            l = i.y - r / 2 - a,
                            u = i.y + r / 2 + a,
                            c = e >= o && s >= e && t >= l && u >= t;
                        return c
                    },
                    o = function (e, t, r, n, i) {
                        var a = e * Math.cos(n) - t * Math.sin(n),
                            o = e * Math.sin(n) + t * Math.cos(n),
                            s = a * r,
                            l = o * r,
                            u = s + i.x,
                            c = l + i.y;
                        return {
                            x: u,
                            y: c
                        }
                    },
                    s = function (e, t, r, n) {
                        for (var i = [], a = 0; a < e.length; a += 2) {
                            var s = e[a],
                                l = e[a + 1];
                            i.push(o(s, l, t, r, n))
                        }
                        return i
                    },
                    l = function (e) {
                        for (var t = [], r = 0; r < e.length; r++) {
                            var n = e[r];
                            t.push(n.x, n.y)
                        }
                        return t
                    },
                    u = function (o, u) {
                        i.string(u) && (u = e[u]), e[o] = a.extend({
                            name: o,
                            points: [-.15, -.3, .15, -.3, .15, .3, -.15, .3],
                            collide: function (e, t, r, i, a, o) {
                                var u = l(s(this.points, r + 2 * o, i, a)),
                                    c = n.pointInsidePolygonPoints(e, t, u);
                                return c
                            },
                            roughCollide: r,
                            draw: function (e, r, n, i) {
                                var a = s(this.points, r, n, i);
                                t.arrowShapeImpl("polygon")(e, a)
                            },
                            spacing: function (e) {
                                return 0
                            },
                            gap: function (e) {
                                return 2 * e.pstyle("width").pfValue
                            }
                        }, u)
                    };
                u("none", {
                    collide: a.falsify,
                    roughCollide: a.falsify,
                    draw: a.noop,
                    spacing: a.zeroify,
                    gap: a.zeroify
                }), u("triangle", {
                    points: [-.15, -.3, 0, 0, .15, -.3]
                }), u("arrow", "triangle"), u("triangle-backcurve", {
                    points: e.triangle.points,
                    controlPoint: [0, -.15],
                    roughCollide: r,
                    draw: function (e, r, n, i) {
                        var a = s(this.points, r, n, i),
                            l = this.controlPoint,
                            u = o(l[0], l[1], r, n, i);
                        t.arrowShapeImpl(this.name)(e, a, u)
                    },
                    gap: function (e) {
                        return e.pstyle("width").pfValue
                    }
                }), u("triangle-tee", {
                    points: [-.15, -.3, 0, 0, .15, -.3, -.15, -.3],
                    pointsTee: [-.15, -.4, -.15, -.5, .15, -.5, .15, -.4],
                    collide: function (e, t, r, i, a, o) {
                        var u = l(s(this.points, r + 2 * o, i, a)),
                            c = l(s(this.pointsTee, r + 2 * o, i, a)),
                            d = n.pointInsidePolygonPoints(e, t, u) || n.pointInsidePolygonPoints(e, t, c);
                        return d
                    },
                    draw: function (e, r, n, i) {
                        var a = s(this.points, r, n, i),
                            o = s(this.pointsTee, r, n, i);
                        t.arrowShapeImpl(this.name)(e, a, o)
                    }
                }), u("vee", {
                    points: [-.15, -.3, 0, 0, .15, -.3, 0, -.15],
                    gap: function (e) {
                        return e.pstyle("width").pfValue
                    }
                }), u("circle", {
                    radius: .15,
                    collide: function (e, t, r, n, i, a) {
                        var o = i,
                            s = Math.pow(o.x - e, 2) + Math.pow(o.y - t, 2) <= Math.pow((r + 2 * a) * this.radius, 2);
                        return s
                    },
                    draw: function (e, r, n, i) {
                        t.arrowShapeImpl(this.name)(e, i.x, i.y, this.radius * r)
                    },
                    spacing: function (e) {
                        return t.getArrowWidth(e.pstyle("width").pfValue) * this.radius
                    }
                }), u("inhibitor", {
                    points: [-.15, 0, -.15, -.1, .15, -.1, .15, 0],
                    spacing: function (e) {
                        return 1
                    },
                    gap: function (e) {
                        return 1
                    }
                }), u("tee", "inhibitor"), u("square", {
                    points: [-.15, 0, .15, 0, .15, -.3, -.15, -.3]
                }), u("diamond", {
                    points: [-.15, -.15, 0, -.3, .15, -.15, 0, 0],
                    gap: function (e) {
                        return e.pstyle("width").pfValue
                    }
                })
            }, t.exports = o
        }, {
            "../../../is": 83,
            "../../../math": 85,
            "../../../util": 100
        }],
        58: [function (e, t, r) {
            "use strict";

            function n(e, t, r) {
                for (var n = function (e, t, r, n) {
                    return i.qbezierAt(e, t, r, n)
                }, a = t._private, o = a.rstyle.bezierPts, s = 0; s < e.bezierProjPcts.length; s++) {
                    var l = e.bezierProjPcts[s];
                    o.push({
                        x: n(r[0], r[2], r[4], l),
                        y: n(r[1], r[3], r[5], l)
                    })
                }
            }
            var i = e("../../../math"),
                a = e("../../../is"),
                o = e("../../../util"),
                s = e("../../../collection/zsort"),
                l = e("../../../window"),
                u = {};
            u.registerCalculationListeners = function () {
                var e = this.cy,
                    t = e.collection(),
                    r = this,
                    n = function (e, r) {
                        t.merge(e);
                        for (var n = 0; n < e.length; n++) {
                            var i = e[n],
                                a = i._private,
                                o = a.rstyle;
                            o.clean = !1, a.bbCache = null;
                            var s = o.dirtyEvents = o.dirtyEvents || {
                                length: 0
                            };
                            s[r.type] || (s[r.type] = !0, s.length++)
                        }
                    };
                r.binder(e).on("position.* style.* free.*", "node", function (t) {
                    var r = t.cyTarget;
                    if (n(r, t), n(r.connectedEdges(), t), e.hasCompoundNodes()) {
                        var i = r.parents();
                        n(i, t), n(i.connectedEdges(), t)
                    }
                }).on("add.* background.*", "node", function (e) {
                    var t = e.cyTarget;
                    n(t, e)
                }).on("add.* style.*", "edge", function (e) {
                    var t = e.cyTarget;
                    n(t, e), n(t.parallelEdges(), e)
                }).on("remove.*", "edge", function (e) {
                    for (var t = e.cyTarget, r = t.parallelEdges(), i = 0; i < r.length; i++) {
                        var a = r[i];
                        a.removed() || n(a, e)
                    }
                });
                var i = function (n) {
                    if (n) {
                        var i = r.onUpdateEleCalcsFns;
                        if (i)
                            for (var a = 0; a < i.length; a++) {
                                var o = i[a];
                                o(n, t)
                            }
                        r.recalculateRenderedStyle(t, !1);
                        for (var a = 0; a < t.length; a++) t[a]._private.rstyle.dirtyEvents = null;
                        t = e.collection()
                    }
                };
                r.beforeRender(i, r.beforeRenderPriorities.eleCalcs)
            }, u.onUpdateEleCalcs = function (e) {
                var t = this.onUpdateEleCalcsFns = this.onUpdateEleCalcsFns || [];
                t.push(e)
            }, u.recalculateRenderedStyle = function (e, t) {
                var r = [],
                    n = [];
                if (!this.destroyed) {
                    void 0 === t && (t = !0);
                    for (var i = 0; i < e.length; i++) {
                        var a = e[i],
                            o = a._private,
                            s = o.rstyle;
                        t && s.clean || a.removed() || ("nodes" === o.group ? n.push(a) : r.push(a), s.clean = !0)
                    }
                    for (var i = 0; i < n.length; i++) {
                        var a = n[i],
                            o = a._private,
                            s = o.rstyle,
                            l = o.position;
                        this.recalculateNodeLabelProjection(a), s.nodeX = l.x, s.nodeY = l.y, s.nodeW = a.pstyle("width").pfValue, s.nodeH = a.pstyle("height").pfValue
                    }
                    this.recalculateEdgeProjections(r);
                    for (var i = 0; i < r.length; i++) {
                        var a = r[i],
                            o = a._private,
                            s = o.rstyle,
                            u = o.rscratch;
                        this.recalculateEdgeLabelProjections(a), s.srcX = u.arrowStartX, s.srcY = u.arrowStartY, s.tgtX = u.arrowEndX, s.tgtY = u.arrowEndY, s.midX = u.midX, s.midY = u.midY, s.labelAngle = u.labelAngle, s.sourceLabelAngle = u.sourceLabelAngle, s.targetLabelAngle = u.targetLabelAngle
                    }
                }
            }, u.projectIntoViewport = function (e, t) {
                var r = this.cy,
                    n = this.findContainerClientCoords(),
                    i = n[0],
                    a = n[1],
                    o = r.pan(),
                    s = r.zoom(),
                    l = (e - i - o.x) / s,
                    u = (t - a - o.y) / s;
                return [l, u]
            }, u.findContainerClientCoords = function () {
                if (this.containerBB) return this.containerBB;
                var e = this.container,
                    t = e.getBoundingClientRect(),
                    r = l.getComputedStyle(e),
                    n = function (e) {
                        return parseFloat(r.getPropertyValue(e))
                    },
                    i = {
                        left: n("padding-left") + n("border-left-width"),
                        right: n("padding-right") + n("border-right-width"),
                        top: n("padding-top") + n("border-top-width"),
                        bottom: n("padding-bottom") + n("border-bottom-width")
                    };
                return this.containerBB = [t.left + i.left, t.top + i.top, t.right - t.left - i.left - i.right, t.bottom - t.top - i.top - i.bottom]
            }, u.invalidateContainerClientCoordsCache = function () {
                this.containerBB = null
            }, u.findNearestElement = function (e, t, r, n) {
                return this.findNearestElements(e, t, r, n)[0]
            }, u.findNearestElements = function (e, t, r, n) {
                function a(e, t) {
                    if (e.isNode()) {
                        if (h) return;
                        h = e, g.push(e)
                    }
                    if (e.isEdge() && (null == t || E > t))
                        if (d) {
                            if (d.pstyle("z-index").value === e.pstyle("z-index").value)
                                for (var r = 0; r < g.length; r++)
                                    if (g[r].isEdge()) {
                                        g[r] = e, d = e, E = null != t ? t : E;
                                        break
                                    }
                        } else g.push(e), d = e, E = null != t ? t : E
                }

                function s(n) {
                    var i = n._private;
                    if ("no" !== n.pstyle("events").strValue) {
                        var o = n.outerWidth() + 2 * x,
                            s = n.outerHeight() + 2 * x,
                            l = o / 2,
                            u = s / 2,
                            c = i.position;
                        if (c.x - l <= e && e <= c.x + l && c.y - u <= t && t <= c.y + u) {
                            var d = !r || n.visible() && !n.transparent();
                            if (r && !d) return;
                            var h = f.nodeShapes[p.getNodeShape(n)];
                            if (h.checkPoint(e, t, 0, o, s, c.x, c.y)) return a(n, 0), !0
                        }
                    }
                }

                function l(n) {
                    var o = n._private;
                    if ("no" !== n.pstyle("events").strValue) {
                        var l, u, c = o.rscratch,
                            d = n.pstyle("width").pfValue,
                            h = d / 2 + b,
                            v = h * h,
                            y = 2 * h,
                            x = o.source,
                            w = o.target,
                            E = !1,
                            _ = function () {
                                if (void 0 !== u) return u;
                                if (!r) return u = !0, !0;
                                var e = n.visible() && !n.transparent();
                                return e ? (u = !0, !0) : (u = !1, !1)
                            };
                        if ("segments" === c.edgeType || "straight" === c.edgeType || "haystack" === c.edgeType) {
                            for (var S = c.allpts, P = 0; P + 3 < S.length; P += 2)
                                if ((E = i.inLineVicinity(e, t, S[P], S[P + 1], S[P + 2], S[P + 3], y)) && _() && v > (l = i.sqdistToFiniteLine(e, t, S[P], S[P + 1], S[P + 2], S[P + 3]))) return a(n, l), !0
                        } else if ("bezier" === c.edgeType || "multibezier" === c.edgeType || "self" === c.edgeType || "compound" === c.edgeType)
                            for (var S = c.allpts, P = 0; P + 5 < c.allpts.length; P += 4)
                                if ((E = i.inBezierVicinity(e, t, S[P], S[P + 1], S[P + 2], S[P + 3], S[P + 4], S[P + 5], y)) && _() && v > (l = i.sqdistToQuadraticBezier(e, t, S[P], S[P + 1], S[P + 2], S[P + 3], S[P + 4], S[P + 5]))) return a(n, l), !0;
                        if (_())
                            for (var x = x || o.source, w = w || o.target, T = p.getArrowWidth(d), k = [{
                                name: "source",
                                x: c.arrowStartX,
                                y: c.arrowStartY,
                                angle: c.srcArrowAngle
                            }, {
                                name: "target",
                                x: c.arrowEndX,
                                y: c.arrowEndY,
                                angle: c.tgtArrowAngle
                            }, {
                                name: "mid-source",
                                x: c.midX,
                                y: c.midY,
                                angle: c.midsrcArrowAngle
                            }, {
                                name: "mid-target",
                                x: c.midX,
                                y: c.midY,
                                angle: c.midtgtArrowAngle
                            }], P = 0; P < k.length; P++) {
                                var D = k[P],
                                    C = f.arrowShapes[n.pstyle(D.name + "-arrow-shape").value];
                                if (C.roughCollide(e, t, T, D.angle, {
                                    x: D.x,
                                    y: D.y
                                }, b) && C.collide(e, t, T, D.angle, {
                                    x: D.x,
                                    y: D.y
                                }, b)) return a(n), !0
                            }
                        m && g.length > 0 && (s(x), s(w))
                    }
                }

                function u(e, t, r) {
                    return o.getPrefixedProperty(e, t, r)
                }

                function c(r, n) {
                    var o, s = r._private,
                        l = w;
                    o = n ? n + "-" : "";
                    var c = r.pstyle(o + "label").value,
                        d = "yes" === r.pstyle("text-events").strValue;
                    if (d && c) {
                        var h = s.rstyle,
                            p = r.pstyle("text-border-width").pfValue,
                            f = u(h, "labelWidth", n) + p / 2 + 2 * l,
                            v = u(h, "labelHeight", n) + p / 2 + 2 * l,
                            g = u(h, "labelX", n),
                            y = u(h, "labelY", n),
                            m = u(s.rscratch, "labelAngle", n),
                            b = g - f / 2,
                            x = g + f / 2,
                            E = y - v / 2,
                            _ = y + v / 2;
                        if (m) {
                            var S = Math.cos(m),
                                P = Math.sin(m),
                                T = function (e, t) {
                                    return e -= g, t -= y, {
                                        x: e * S - t * P + g,
                                        y: e * P + t * S + y
                                    }
                                },
                                k = T(b, E),
                                D = T(b, _),
                                C = T(x, E),
                                M = T(x, _),
                                N = [k.x, k.y, C.x, C.y, M.x, M.y, D.x, D.y];
                            if (i.pointInsidePolygonPoints(e, t, N)) return a(r), !0
                        } else {
                            var B = {
                                w: f,
                                h: v,
                                x1: b,
                                x2: x,
                                y1: E,
                                y2: _
                            };
                            if (i.inBoundingBox(B, e, t)) return a(r), !0
                        }
                    }
                }
                for (var d, h, p = this, f = this, v = f.getCachedZSortedEles(), g = [], y = f.cy.zoom(), m = f.cy.hasCompoundNodes(), b = (n ? 24 : 8) / y, x = (n ? 8 : 2) / y, w = (n ? 8 : 2) / y, E = 1 / 0, _ = v.length - 1; _ >= 0; _--) {
                    var S = v[_];
                    S.isNode() ? s(S) || c(S) : l(S) || c(S) || c(S, "source") || c(S, "target")
                }
                return g
            }, u.getAllInBox = function (e, t, r, n) {
                var a = this.getCachedZSortedEles(),
                    o = a.nodes,
                    s = a.edges,
                    l = [],
                    u = Math.min(e, r),
                    c = Math.max(e, r),
                    d = Math.min(t, n),
                    h = Math.max(t, n);
                e = u, r = c, t = d, n = h;
                for (var p = i.makeBoundingBox({
                    x1: e,
                    y1: t,
                    x2: r,
                    y2: n
                }), f = 0; f < o.length; f++) {
                    var v = o[f],
                        g = v.boundingBox({
                            includeNodes: !0,
                            includeEdges: !1,
                            includeLabels: !1,
                            includeShadows: !1
                        });
                    i.boundingBoxesIntersect(p, g) && l.push(o[f])
                }
                for (var y = 0; y < s.length; y++) {
                    var m = s[y],
                        b = m._private,
                        x = b.rscratch;
                    if ((null == x.startX || null == x.startY || i.inBoundingBox(p, x.startX, x.startY)) && (null == x.endX || null == x.endY || i.inBoundingBox(p, x.endX, x.endY)))
                        if ("bezier" === x.edgeType || "multibezier" === x.edgeType || "self" === x.edgeType || "compound" === x.edgeType || "segments" === x.edgeType || "haystack" === x.edgeType) {
                            for (var w = b.rstyle.bezierPts || b.rstyle.linePts || b.rstyle.haystackPts, E = !0, f = 0; f < w.length; f++)
                                if (!i.pointInBoundingBox(p, w[f])) {
                                    E = !1;
                                    break
                                }
                            E && l.push(m)
                        } else "haystack" !== x.edgeType && "straight" !== x.edgeType || l.push(m)
                }
                return l
            }, u.getNodeShape = function (e) {
                var t = this,
                    r = e.pstyle("shape").value;
                if (e.isParent()) return "rectangle" === r || "roundrectangle" === r ? r : "rectangle";
                if ("polygon" === r) {
                    var n = e.pstyle("shape-polygon-points").value;
                    return t.nodeShapes.makePolygon(n).name
                }
                return r
            }, u.updateCachedZSortedEles = function () {
                this.getCachedZSortedEles(!0)
            }, u.updateCachedGrabbedEles = function () {
                var e = this.cachedZSortedEles;
                e.drag = [], e.nondrag = [];
                for (var t = [], r = 0; r < e.length; r++) {
                    var n = e[r],
                        i = n._private.rscratch;
                    n.grabbed() && !n.isParent() ? t.push(n) : i.inDragLayer ? e.drag.push(n) : e.nondrag.push(n)
                }
                for (var r = 0; r < t.length; r++) {
                    var n = t[r];
                    e.drag.push(n)
                }
            }, u.getCachedZSortedEles = function (e) {
                if (e || !this.cachedZSortedEles) {
                    var t = this.cy.mutableElements(),
                        r = [];
                    r.nodes = [], r.edges = [];
                    for (var n = 0; n < t.length; n++) {
                        var i = t[n];
                        (i.animated() || i.visible() && !i.transparent()) && (r.push(i), i.isNode() ? r.nodes.push(i) : r.edges.push(i))
                    }
                    r.sort(s), this.cachedZSortedEles = r, this.updateCachedGrabbedEles()
                } else r = this.cachedZSortedEles;
                return r
            }, u.projectLines = function (e) {
                var t = e._private,
                    r = t.rscratch,
                    i = r.edgeType;
                if (t.rstyle.bezierPts = null, t.rstyle.linePts = null, t.rstyle.haystackPts = null, "multibezier" === i || "bezier" === i || "self" === i || "compound" === i)
                    for (var a = (t.rstyle.bezierPts = [], 0); a + 5 < r.allpts.length; a += 4) n(this, e, r.allpts.slice(a, a + 6));
                else if ("segments" === i)
                    for (var o = t.rstyle.linePts = [], a = 0; a + 1 < r.allpts.length; a += 2) o.push({
                        x: r.allpts[a],
                        y: r.allpts[a + 1]
                    });
                else if ("haystack" === i) {
                    var s = r.haystackPts;
                    t.rstyle.haystackPts = [{
                        x: s[0],
                        y: s[1]
                    }, {
                        x: s[2],
                        y: s[3]
                    }]
                }
                t.rstyle.arrowWidth = this.getArrowWidth(e.pstyle("width").pfValue) * this.arrowShapeWidth
            }, u.projectBezier = u.projectLines, u.recalculateNodeLabelProjection = function (e) {
                var t = e.pstyle("label").strValue;
                if (!a.emptyString(t)) {
                    var r, n, i = e._private,
                        o = e.width(),
                        s = e.height(),
                        l = e.pstyle("padding").pfValue,
                        u = i.position,
                        c = e.pstyle("text-halign").strValue,
                        d = e.pstyle("text-valign").strValue,
                        h = i.rscratch,
                        p = i.rstyle;
                    switch (c) {
                        case "left":
                            r = u.x - o / 2 - l;
                            break;
                        case "right":
                            r = u.x + o / 2 + l;
                            break;
                        default:
                            r = u.x
                    }
                    switch (d) {
                        case "top":
                            n = u.y - s / 2 - l;
                            break;
                        case "bottom":
                            n = u.y + s / 2 + l;
                            break;
                        default:
                            n = u.y
                    }
                    h.labelX = r, h.labelY = n, p.labelX = r, p.labelY = n, this.applyLabelDimensions(e)
                }
            }, u.recalculateEdgeLabelProjections = function (e) {
                var t, r = e._private,
                    n = r.rscratch,
                    a = this,
                    s = {
                        mid: e.pstyle("label").strValue,
                        source: e.pstyle("source-label").strValue,
                        target: e.pstyle("target-label").strValue
                    };
                if (s.mid || s.source || s.target) {
                    t = {
                        x: n.midX,
                        y: n.midY
                    };
                    var l = function (e, t, n) {
                        o.setPrefixedProperty(r.rscratch, e, t, n), o.setPrefixedProperty(r.rstyle, e, t, n)
                    };
                    l("labelX", null, t.x), l("labelY", null, t.y);
                    var u = function () {
                        function e(e, t, r, n, a) {
                            var o = i.dist(t, r),
                                s = e.segments[e.segments.length - 1],
                                l = {
                                    p0: t,
                                    p1: r,
                                    t0: n,
                                    t1: a,
                                    startDist: s ? s.startDist + s.length : 0,
                                    length: o
                                };
                            e.segments.push(l), e.length += o
                        }
                        if (u.cache) return u.cache;
                        for (var t = [], o = 0; o + 5 < n.allpts.length; o += 4) {
                            var s = {
                                x: n.allpts[o],
                                y: n.allpts[o + 1]
                            },
                                l = {
                                    x: n.allpts[o + 2],
                                    y: n.allpts[o + 3]
                                },
                                c = {
                                    x: n.allpts[o + 4],
                                    y: n.allpts[o + 5]
                                };
                            t.push({
                                p0: s,
                                p1: l,
                                p2: c,
                                startDist: 0,
                                length: 0,
                                segments: []
                            })
                        }
                        for (var d = r.rstyle.bezierPts, h = a.bezierProjPcts.length, o = 0; o < t.length; o++) {
                            var p = t[o],
                                f = t[o - 1];
                            f && (p.startDist = f.startDist + f.length), e(p, p.p0, d[o * h], 0, a.bezierProjPcts[0]);
                            for (var v = 0; h - 1 > v; v++) e(p, d[o * h + v], d[o * h + v + 1], a.bezierProjPcts[v], a.bezierProjPcts[v + 1]);
                            e(p, d[o * h + h - 1], p.p2, a.bezierProjPcts[h - 1], 1)
                        }
                        return u.cache = t
                    },
                        c = function (r) {
                            var a, o = "source" === r;
                            if (s[r]) {
                                var c = e.pstyle(r + "-text-offset").pfValue,
                                    d = function (e, t) {
                                        var r = t.x - e.x,
                                            n = t.y - e.y;
                                        return Math.atan(n / r)
                                    },
                                    h = function (e, t, r, n) {
                                        var a = i.bound(0, n - .001, 1),
                                            o = i.bound(0, n + .001, 1),
                                            s = i.qbezierPtAt(e, t, r, a),
                                            l = i.qbezierPtAt(e, t, r, o);
                                        return d(s, l)
                                    };
                                switch (n.edgeType) {
                                    case "self":
                                    case "compound":
                                    case "bezier":
                                    case "multibezier":
                                        for (var p, f = u(), v = 0, g = 0, y = 0; y < f.length; y++) {
                                            for (var m = f[o ? y : f.length - 1 - y], b = 0; b < m.segments.length; b++) {
                                                var x = m.segments[o ? b : m.segments.length - 1 - b],
                                                    w = y === f.length - 1 && b === m.segments.length - 1;
                                                if (v = g, g += x.length, g >= c || w) {
                                                    p = {
                                                        cp: m,
                                                        segment: x
                                                    };
                                                    break
                                                }
                                            }
                                            if (p) break
                                        }
                                        var m = p.cp,
                                            x = p.segment,
                                            E = (c - v) / x.length,
                                            _ = x.t1 - x.t0,
                                            S = o ? x.t0 + _ * E : x.t1 - _ * E;
                                        S = i.bound(0, S, 1), t = i.qbezierPtAt(m.p0, m.p1, m.p2, S), a = h(m.p0, m.p1, m.p2, S, t);
                                        break;
                                    case "straight":
                                    case "segments":
                                    case "haystack":
                                        for (var P, T, k, D, C = 0, M = n.allpts.length, y = 0; M > y + 3 && (o ? (k = {
                                            x: n.allpts[y],
                                            y: n.allpts[y + 1]
                                        }, D = {
                                            x: n.allpts[y + 2],
                                            y: n.allpts[y + 3]
                                        }) : (k = {
                                            x: n.allpts[M - 2 - y],
                                            y: n.allpts[M - 1 - y]
                                        }, D = {
                                            x: n.allpts[M - 4 - y],
                                            y: n.allpts[M - 3 - y]
                                        }), P = i.dist(k, D), T = C, C += P, !(C >= c)); y += 2);
                                        var N = c - T,
                                            S = N / P;
                                        S = i.bound(0, S, 1), t = i.lineAt(k, D, S), a = d(k, D)
                                }
                                l("labelX", r, t.x), l("labelY", r, t.y), l("labelAutoAngle", r, a)
                            }
                        };
                    c("source"), c("target"), this.applyLabelDimensions(e)
                }
            }, u.applyLabelDimensions = function (e) {
                this.applyPrefixedLabelDimensions(e), e.isEdge() && (this.applyPrefixedLabelDimensions(e, "source"), this.applyPrefixedLabelDimensions(e, "target"))
            }, u.applyPrefixedLabelDimensions = function (e, t) {
                var r = e._private,
                    n = this.getLabelText(e, t),
                    i = this.calculateLabelDimensions(e, n);
                o.setPrefixedProperty(r.rstyle, "labelWidth", t, i.width), o.setPrefixedProperty(r.rscratch, "labelWidth", t, i.width), o.setPrefixedProperty(r.rstyle, "labelHeight", t, i.height), o.setPrefixedProperty(r.rscratch, "labelHeight", t, i.height)
            }, u.getLabelText = function (e, t) {
                var r = e._private,
                    n = t ? t + "-" : "",
                    i = e.pstyle(n + "label").strValue,
                    a = e.pstyle("text-transform").value,
                    s = function (e, n) {
                        return n ? (o.setPrefixedProperty(r.rscratch, e, t, n), n) : o.getPrefixedProperty(r.rscratch, e, t)
                    };
                if ("none" == a || ("uppercase" == a ? i = i.toUpperCase() : "lowercase" == a && (i = i.toLowerCase())), "wrap" === e.pstyle("text-wrap").value) {
                    var l = s("labelKey");
                    if (l && s("labelWrapKey") === l) return s("labelWrapCachedText");
                    for (var u = i.split("\n"), c = e.pstyle("text-max-width").pfValue, d = [], h = 0; h < u.length; h++) {
                        var p = u[h],
                            f = this.calculateLabelDimensions(e, p, "line=" + p),
                            v = f.width;
                        if (v > c) {
                            for (var g = p.split(/\s+/), y = "", m = 0; m < g.length; m++) {
                                var b = g[m],
                                    x = 0 === y.length ? b : y + " " + b,
                                    w = this.calculateLabelDimensions(e, x, "testLine=" + x),
                                    E = w.width;
                                c >= E ? y += b + " " : (d.push(y), y = b + " ")
                            }
                            y.match(/^\s+$/) || d.push(y)
                        } else d.push(p)
                    }
                    s("labelWrapCachedLines", d), i = s("labelWrapCachedText", d.join("\n")), s("labelWrapKey", l)
                }
                return i
            }, u.calculateLabelDimensions = function (e, t, r) {
                var n = this,
                    i = e._private.labelStyleKey + "$@$" + t;
                r && (i += "$@$" + r);
                var a = n.labelDimCache || (n.labelDimCache = {});
                if (a[i]) return a[i];
                var o = 1,
                    s = e.pstyle("font-style").strValue,
                    l = o * e.pstyle("font-size").pfValue + "px",
                    u = e.pstyle("font-family").strValue,
                    c = e.pstyle("font-weight").strValue,
                    d = this.labelCalcDiv;
                d || (d = this.labelCalcDiv = document.createElement("div"), document.body.appendChild(d));
                var h = d.style;
                return h.fontFamily = u, h.fontStyle = s, h.fontSize = l, h.fontWeight = c, h.position = "absolute", h.left = "-9999px", h.top = "-9999px", h.zIndex = "-1", h.visibility = "hidden", h.pointerEvents = "none", h.padding = "0", h.lineHeight = "1", "wrap" === e.pstyle("text-wrap").value ? h.whiteSpace = "pre" : h.whiteSpace = "normal", d.textContent = t, a[i] = {
                    width: Math.ceil(d.clientWidth / o),
                    height: Math.ceil(d.clientHeight / o)
                }, a[i]
            }, u.recalculateEdgeProjections = function (e) {
                this.findEdgeControlPoints(e)
            }, u.findEdgeControlPoints = function (e) {
                if (e && 0 !== e.length) {
                    for (var t, r = this, n = r.cy, o = n.hasCompoundNodes(), s = {}, l = [], u = [], c = 0; c < e.length; c++) {
                        var d = e[c],
                            h = d._private,
                            p = h.data,
                            f = d.pstyle("curve-style").value,
                            v = "unbundled-bezier" === f || "segments" === f;
                        if ("none" !== d.pstyle("display").value)
                            if ("haystack" !== f) {
                                var g = p.source,
                                    y = p.target;
                                t = g > y ? y + "$-$" + g : g + "$-$" + y, v && (t = "unbundled$-$" + p.id), null == s[t] && (s[t] = [], l.push(t)), s[t].push(d), v && (s[t].hasUnbundled = !0)
                            } else u.push(d)
                    }
                    for (var m, b, x, w, E, _, S, P, T, k, D, C, M, N, B = 0; B < l.length; B++) {
                        t = l[B];
                        var z = s[t];
                        if (z.sort(function (e, t) {
                            return e.poolIndex() - t.poolIndex()
                        }), m = z[0]._private.source, b = z[0]._private.target, !z.hasUnbundled && m.id() > b.id()) {
                            var I = m;
                            m = b, b = I
                        }
                        if (x = m._private, w = b._private, E = x.position, _ = w.position, S = m.outerWidth(), P = m.outerHeight(), T = b.outerWidth(), k = b.outerHeight(), D = r.nodeShapes[this.getNodeShape(m)], C = r.nodeShapes[this.getNodeShape(b)], N = !1, z.length > 1 && m !== b || z.hasUnbundled) {
                            var L = D.intersectLine(E.x, E.y, S, P, _.x, _.y, 0),
                                O = C.intersectLine(_.x, _.y, T, k, E.x, E.y, 0),
                                A = {
                                    x1: L[0],
                                    x2: O[0],
                                    y1: L[1],
                                    y2: O[1]
                                },
                                R = {
                                    x1: E.x,
                                    x2: _.x,
                                    y1: E.y,
                                    y2: _.y
                                },
                                q = O[1] - L[1],
                                V = O[0] - L[0],
                                F = Math.sqrt(V * V + q * q),
                                j = {
                                    x: V,
                                    y: q
                                },
                                X = {
                                    x: j.x / F,
                                    y: j.y / F
                                };
                            M = {
                                x: -X.y,
                                y: X.x
                            }, C.checkPoint(L[0], L[1], 0, T, k, _.x, _.y) && D.checkPoint(O[0], O[1], 0, S, P, E.x, E.y) && (M = {}, N = !0)
                        }
                        for (var d, Y, W, c = 0; c < z.length; c++) {
                            d = z[c], Y = d._private, W = Y.rscratch;
                            var $ = W.lastEdgeIndex,
                                H = c,
                                U = W.lastNumEdges,
                                Z = z.length,
                                f = d.pstyle("curve-style").value,
                                G = d.pstyle("control-point-distances"),
                                Q = d.pstyle("control-point-weights"),
                                K = G && Q ? Math.min(G.value.length, Q.value.length) : 1,
                                J = d.pstyle("control-point-step-size").pfValue,
                                ee = G ? G.pfValue[0] : void 0,
                                te = Q.value[0],
                                v = "unbundled-bezier" === f || "segments" === f,
                                re = d.pstyle("edge-distances").value,
                                ne = d.pstyle("segment-weights"),
                                ie = d.pstyle("segment-distances"),
                                ae = Math.min(ne.pfValue.length, ie.pfValue.length),
                                oe = W.lastSrcCtlPtX,
                                se = E.x,
                                le = W.lastSrcCtlPtY,
                                ue = E.y,
                                ce = W.lastSrcCtlPtW,
                                de = m.outerWidth(),
                                he = W.lastSrcCtlPtH,
                                pe = m.outerHeight(),
                                fe = W.lastTgtCtlPtX,
                                ve = _.x,
                                ge = W.lastTgtCtlPtY,
                                ye = _.y,
                                me = W.lastTgtCtlPtW,
                                be = b.outerWidth(),
                                xe = W.lastTgtCtlPtH,
                                we = b.outerHeight(),
                                Ee = W.lastCurveStyle,
                                _e = f,
                                Se = W.lastCtrlptDists,
                                Pe = G ? G.strValue : null,
                                Te = W.lastCtrlptWs,
                                ke = Q.strValue,
                                De = W.lastSegmentWs,
                                Ce = ne.strValue,
                                Me = W.lastSegmentDs,
                                Ne = ie.strValue,
                                Be = W.lastStepSize,
                                ze = J,
                                Ie = W.lastEdgeDistances,
                                Le = re;
                            if (N ? W.badBezier = !0 : W.badBezier = !1, oe !== se || le !== ue || ce !== de || he !== pe || fe !== ve || ge !== ye || me !== be || xe !== we || Ee !== _e || Se !== Pe || Te !== ke || De !== Ce || Me !== Ne || Be !== ze || Ie !== Le || !($ === H && U === Z || v)) {
                                if (W.lastSrcCtlPtX = se, W.lastSrcCtlPtY = ue, W.lastSrcCtlPtW = de, W.lastSrcCtlPtH = pe, W.lastTgtCtlPtX = ve, W.lastTgtCtlPtY = ye, W.lastTgtCtlPtW = be, W.lastTgtCtlPtH = we, W.lastEdgeIndex = H, W.lastNumEdges = Z, W.lastCurveStyle = _e, W.lastCtrlptDists = Pe, W.lastCtrlptWs = ke, W.lastSegmentDs = Ne, W.lastSegmentWs = Ce, W.lastStepSize = ze, W.lastEdgeDistances = Le, m === b) {
                                    W.edgeType = "self";
                                    var Oe = c,
                                        Ae = J;
                                    v && (Oe = 0, Ae = ee), W.ctrlpts = [E.x, E.y - (1 + Math.pow(P, 1.12) / 100) * Ae * (Oe / 3 + 1), E.x - (1 + Math.pow(S, 1.12) / 100) * Ae * (Oe / 3 + 1), E.y]
                                } else if (o && (m.isParent() || m.isChild() || b.isParent() || b.isChild()) && (m.parents().anySame(b) || b.parents().anySame(m))) {
                                    W.edgeType = "compound", W.badBezier = !1;
                                    var Oe = c,
                                        Ae = J;
                                    v && (Oe = 0, Ae = ee);
                                    var Re = 50,
                                        qe = {
                                            x: E.x - S / 2,
                                            y: E.y - P / 2
                                        },
                                        Ve = {
                                            x: _.x - T / 2,
                                            y: _.y - k / 2
                                        },
                                        Fe = {
                                            x: Math.min(qe.x, Ve.x),
                                            y: Math.min(qe.y, Ve.y)
                                        },
                                        je = .5,
                                        Xe = Math.max(je, Math.log(.01 * S)),
                                        Ye = Math.max(je, Math.log(.01 * T));
                                    W.ctrlpts = [Fe.x, Fe.y - (1 + Math.pow(Re, 1.12) / 100) * Ae * (Oe / 3 + 1) * Xe, Fe.x - (1 + Math.pow(Re, 1.12) / 100) * Ae * (Oe / 3 + 1) * Ye, Fe.y]
                                } else if ("segments" === f) {
                                    W.edgeType = "segments", W.segpts = [];
                                    for (var We = 0; ae > We; We++) {
                                        var $e = ne.pfValue[We],
                                            He = ie.pfValue[We],
                                            Ue = 1 - $e,
                                            Ze = $e,
                                            Ge = "node-position" === re ? R : A,
                                            Qe = {
                                                x: Ge.x1 * Ue + Ge.x2 * Ze,
                                                y: Ge.y1 * Ue + Ge.y2 * Ze
                                            };
                                        W.segpts.push(Qe.x + M.x * He, Qe.y + M.y * He)
                                    }
                                } else if (z.length % 2 !== 1 || c !== Math.floor(z.length / 2) || v) {
                                    var Ke = v;
                                    W.edgeType = Ke ? "multibezier" : "bezier", W.ctrlpts = [];
                                    for (var Je = 0; K > Je; Je++) {
                                        var et, tt = (.5 - z.length / 2 + c) * J,
                                            rt = i.signum(tt);
                                        Ke && (ee = G ? G.pfValue[Je] : J, te = Q.value[Je]), et = v ? ee : void 0 !== ee ? rt * ee : void 0;
                                        var nt = void 0 !== et ? et : tt,
                                            Ue = 1 - te,
                                            Ze = te,
                                            Ge = "node-position" === re ? R : A,
                                            Qe = {
                                                x: Ge.x1 * Ue + Ge.x2 * Ze,
                                                y: Ge.y1 * Ue + Ge.y2 * Ze
                                            };
                                        W.ctrlpts.push(Qe.x + M.x * nt, Qe.y + M.y * nt)
                                    }
                                } else W.edgeType = "straight";
                                this.findEndpoints(d);
                                var it = !a.number(W.startX) || !a.number(W.startY),
                                    at = !a.number(W.arrowStartX) || !a.number(W.arrowStartY),
                                    ot = !a.number(W.endX) || !a.number(W.endY),
                                    st = !a.number(W.arrowEndX) || !a.number(W.arrowEndY),
                                    lt = 3,
                                    ut = this.getArrowWidth(d.pstyle("width").pfValue) * this.arrowShapeWidth,
                                    ct = lt * ut;
                                if ("bezier" === W.edgeType) {
                                    var dt = i.dist({
                                        x: W.ctrlpts[0],
                                        y: W.ctrlpts[1]
                                    }, {
                                            x: W.startX,
                                            y: W.startY
                                        }),
                                        ht = ct > dt,
                                        pt = i.dist({
                                            x: W.ctrlpts[0],
                                            y: W.ctrlpts[1]
                                        }, {
                                                x: W.endX,
                                                y: W.endY
                                            }),
                                        ft = ct > pt,
                                        vt = !1;
                                    if (it || at || ht) {
                                        vt = !0;
                                        var gt = {
                                            x: W.ctrlpts[0] - E.x,
                                            y: W.ctrlpts[1] - E.y
                                        },
                                            yt = Math.sqrt(gt.x * gt.x + gt.y * gt.y),
                                            mt = {
                                                x: gt.x / yt,
                                                y: gt.y / yt
                                            },
                                            bt = Math.max(S, P),
                                            xt = {
                                                x: W.ctrlpts[0] + 2 * mt.x * bt,
                                                y: W.ctrlpts[1] + 2 * mt.y * bt
                                            },
                                            wt = D.intersectLine(E.x, E.y, S, P, xt.x, xt.y, 0);
                                        ht ? (W.ctrlpts[0] = W.ctrlpts[0] + mt.x * (ct - dt), W.ctrlpts[1] = W.ctrlpts[1] + mt.y * (ct - dt)) : (W.ctrlpts[0] = wt[0] + mt.x * ct, W.ctrlpts[1] = wt[1] + mt.y * ct)
                                    }
                                    if (ot || st || ft) {
                                        vt = !0;
                                        var gt = {
                                            x: W.ctrlpts[0] - _.x,
                                            y: W.ctrlpts[1] - _.y
                                        },
                                            yt = Math.sqrt(gt.x * gt.x + gt.y * gt.y),
                                            mt = {
                                                x: gt.x / yt,
                                                y: gt.y / yt
                                            },
                                            bt = Math.max(S, P),
                                            xt = {
                                                x: W.ctrlpts[0] + 2 * mt.x * bt,
                                                y: W.ctrlpts[1] + 2 * mt.y * bt
                                            },
                                            Et = C.intersectLine(_.x, _.y, T, k, xt.x, xt.y, 0);
                                        ft ? (W.ctrlpts[0] = W.ctrlpts[0] + mt.x * (ct - pt), W.ctrlpts[1] = W.ctrlpts[1] + mt.y * (ct - pt)) : (W.ctrlpts[0] = Et[0] + mt.x * ct, W.ctrlpts[1] = Et[1] + mt.y * ct)
                                    }
                                    vt && this.findEndpoints(d)
                                }
                                if ("multibezier" === W.edgeType || "bezier" === W.edgeType || "self" === W.edgeType || "compound" === W.edgeType) {
                                    W.allpts = [], W.allpts.push(W.startX, W.startY);
                                    for (var Je = 0; Je + 1 < W.ctrlpts.length; Je += 2) W.allpts.push(W.ctrlpts[Je], W.ctrlpts[Je + 1]), Je + 3 < W.ctrlpts.length && W.allpts.push((W.ctrlpts[Je] + W.ctrlpts[Je + 2]) / 2, (W.ctrlpts[Je + 1] + W.ctrlpts[Je + 3]) / 2);
                                    W.allpts.push(W.endX, W.endY);
                                    var _t, St;
                                    W.ctrlpts.length / 2 % 2 === 0 ? (_t = W.allpts.length / 2 - 1, W.midX = W.allpts[_t], W.midY = W.allpts[_t + 1]) : (_t = W.allpts.length / 2 - 3, St = .5, W.midX = i.qbezierAt(W.allpts[_t], W.allpts[_t + 2], W.allpts[_t + 4], St), W.midY = i.qbezierAt(W.allpts[_t + 1], W.allpts[_t + 3], W.allpts[_t + 5], St))
                                } else if ("straight" === W.edgeType) W.allpts = [W.startX, W.startY, W.endX, W.endY], W.midX = (W.startX + W.endX + W.arrowStartX + W.arrowEndX) / 4, W.midY = (W.startY + W.endY + W.arrowStartY + W.arrowEndY) / 4;
                                else if ("segments" === W.edgeType)
                                    if (W.allpts = [], W.allpts.push(W.startX, W.startY), W.allpts.push.apply(W.allpts, W.segpts), W.allpts.push(W.endX, W.endY), W.segpts.length % 4 === 0) {
                                        var Pt = W.segpts.length / 2,
                                            Tt = Pt - 2;
                                        W.midX = (W.segpts[Tt] + W.segpts[Pt]) / 2, W.midY = (W.segpts[Tt + 1] + W.segpts[Pt + 1]) / 2
                                    } else {
                                        var Tt = W.segpts.length / 2 - 1;
                                        W.midX = W.segpts[Tt], W.midY = W.segpts[Tt + 1]
                                    }
                                this.projectLines(d), this.calculateArrowAngles(d), this.recalculateEdgeLabelProjections(d), this.calculateLabelAngles(d)
                            }
                        }
                    }
                    for (var c = 0; c < u.length; c++) {
                        var d = u[c],
                            h = d._private,
                            kt = h.rscratch,
                            W = kt;
                        if (!kt.haystack) {
                            var Dt = 2 * Math.random() * Math.PI;
                            kt.source = {
                                x: Math.cos(Dt),
                                y: Math.sin(Dt)
                            };
                            var Dt = 2 * Math.random() * Math.PI;
                            kt.target = {
                                x: Math.cos(Dt),
                                y: Math.sin(Dt)
                            }
                        }
                        var m = h.source,
                            b = h.target,
                            E = m._private.position,
                            _ = b._private.position,
                            S = m.width(),
                            T = b.width(),
                            P = m.height(),
                            k = b.height(),
                            bt = d.pstyle("haystack-radius").value,
                            Ct = bt / 2;
                        W.haystackPts = W.allpts = [W.source.x * S * Ct + E.x, W.source.y * P * Ct + E.y, W.target.x * T * Ct + _.x, W.target.y * k * Ct + _.y], W.midX = (W.allpts[0] + W.allpts[2]) / 2, W.midY = (W.allpts[1] + W.allpts[3]) / 2, kt.edgeType = "haystack", kt.haystack = !0, this.projectLines(d), this.calculateArrowAngles(d), this.recalculateEdgeLabelProjections(d), this.calculateLabelAngles(d)
                    }
                    return s
                }
            };
            var c = function (e, t) {
                return Math.atan2(t, e) - Math.PI / 2
            };
            u.calculateArrowAngles = function (e) {
                var t, r, n, a, o, s, l = e._private.rscratch,
                    u = "haystack" === l.edgeType,
                    d = "multibezier" === l.edgeType,
                    h = "segments" === l.edgeType,
                    p = "compound" === l.edgeType,
                    f = "self" === l.edgeType,
                    v = e._private.source._private.position,
                    g = e._private.target._private.position;
                u ? (n = l.haystackPts[0], a = l.haystackPts[1], o = l.haystackPts[2], s = l.haystackPts[3]) : (n = l.arrowStartX, a = l.arrowStartY, o = l.arrowEndX, s = l.arrowEndY), t = v.x - n, r = v.y - a, l.srcArrowAngle = c(t, r);
                var y = l.midX,
                    m = l.midY;
                if (u && (y = (n + o) / 2, m = (a + s) / 2), t = o - n, r = s - a, f) t = -1, r = 1;
                else if (h) {
                    var b = l.allpts;
                    if (b.length / 2 % 2 === 0) {
                        var x = b.length / 2,
                            w = x - 2;
                        t = b[x] - b[w], r = b[x + 1] - b[w + 1]
                    } else {
                        var x = b.length / 2 - 1,
                            w = x - 2,
                            E = x + 2;
                        t = b[x] - b[w], r = b[x + 1] - b[w + 1]
                    }
                } else if (d || p) {
                    var _, S, P, T, b = l.allpts,
                        k = l.ctrlpts;
                    if (k.length / 2 % 2 === 0) {
                        var D = b.length / 2 - 1,
                            C = D + 2,
                            M = C + 2;
                        _ = i.qbezierAt(b[D], b[C], b[M], 0), S = i.qbezierAt(b[D + 1], b[C + 1], b[M + 1], 0), P = i.qbezierAt(b[D], b[C], b[M], 1e-4), T = i.qbezierAt(b[D + 1], b[C + 1], b[M + 1], 1e-4)
                    } else {
                        var C = b.length / 2 - 1,
                            D = C - 2,
                            M = C + 2;
                        _ = i.qbezierAt(b[D], b[C], b[M], .4999), S = i.qbezierAt(b[D + 1], b[C + 1], b[M + 1], .4999), P = i.qbezierAt(b[D], b[C], b[M], .5), T = i.qbezierAt(b[D + 1], b[C + 1], b[M + 1], .5)
                    }
                    t = P - _, r = T - S
                }
                if (l.midtgtArrowAngle = c(t, r), l.midDispX = t, l.midDispY = r, t *= -1, r *= -1, h) {
                    var b = l.allpts;
                    if (b.length / 2 % 2 === 0);
                    else {
                        var x = b.length / 2 - 1,
                            E = x + 2;
                        t = -(b[E] - b[x]), r = -(b[E + 1] - b[x + 1])
                    }
                }
                l.midsrcArrowAngle = c(t, r), t = g.x - o, r = g.y - s, l.tgtArrowAngle = c(t, r)
            }, u.calculateLabelAngles = function (e) {
                var t = e._private,
                    r = t.rscratch,
                    n = e.isEdge(),
                    i = e.pstyle("text-rotation"),
                    a = i.strValue;
                "none" === a ? r.labelAngle = r.sourceLabelAngle = r.targetLabelAngle = 0 : n && "autorotate" === a ? (r.labelAngle = Math.atan(r.midDispY / r.midDispX), r.sourceLabelAngle = r.sourceLabelAutoAngle, r.targetLabelAngle = r.targetLabelAutoAngle) : "autorotate" === a ? r.labelAngle = r.sourceLabelAngle = r.targetLabelAngle = 0 : r.labelAngle = r.sourceLabelAngle = r.targetLabelAngle = i.pfValue
            }, u.findEndpoints = function (e) {
                var t, r, n, o = this,
                    s = e.source()[0],
                    l = e.target()[0],
                    u = s._private,
                    c = l._private,
                    d = u.position,
                    h = c.position,
                    p = e.pstyle("target-arrow-shape").value,
                    f = e.pstyle("source-arrow-shape").value,
                    v = e._private.rscratch,
                    g = v.edgeType,
                    y = "bezier" === g || "multibezier" === g || "self" === g || "compound" === g,
                    m = "bezier" !== g,
                    b = "straight" === g || "segments" === g,
                    x = "segments" === g,
                    w = y || m || b;
                if (y) {
                    var E = [v.ctrlpts[0], v.ctrlpts[1]],
                        _ = m ? [v.ctrlpts[v.ctrlpts.length - 2], v.ctrlpts[v.ctrlpts.length - 1]] : E;
                    r = _, n = E
                } else if (b) {
                    var S = x ? v.segpts.slice(0, 2) : [h.x, h.y],
                        P = x ? v.segpts.slice(v.segpts.length - 2) : [d.x, d.y];
                    r = P, n = S
                }
                t = o.nodeShapes[this.getNodeShape(l)].intersectLine(h.x, h.y, l.outerWidth(), l.outerHeight(), r[0], r[1], 0);
                var T = i.shortenIntersection(t, r, o.arrowShapes[p].spacing(e)),
                    k = i.shortenIntersection(t, r, o.arrowShapes[p].gap(e));
                v.endX = k[0], v.endY = k[1], v.arrowEndX = T[0], v.arrowEndY = T[1], t = o.nodeShapes[this.getNodeShape(s)].intersectLine(d.x, d.y, s.outerWidth(), s.outerHeight(), n[0], n[1], 0);
                var D = i.shortenIntersection(t, n, o.arrowShapes[f].spacing(e)),
                    C = i.shortenIntersection(t, n, o.arrowShapes[f].gap(e));
                v.startX = C[0], v.startY = C[1], v.arrowStartX = D[0], v.arrowStartY = D[1], w && (a.number(v.startX) && a.number(v.startY) && a.number(v.endX) && a.number(v.endY) ? v.badLine = !1 : v.badLine = !0)
            }, u.getArrowWidth = u.getArrowHeight = function (e) {
                var t = this.arrowWidthCache = this.arrowWidthCache || {},
                    r = t[e];
                return r ? r : (r = Math.max(Math.pow(13.37 * e, .9), 29), t[e] = r, r)
            }, t.exports = u
        }, {
            "../../../collection/zsort": 32,
            "../../../is": 83,
            "../../../math": 85,
            "../../../util": 100,
            "../../../window": 107
        }],
        59: [function (e, t, r) {
            "use strict";
            var n = {};
            n.getCachedImage = function (e, t, r) {
                var n = this,
                    i = n.imageCache = n.imageCache || {},
                    a = i[e];
                if (a) return a.image.complete || a.image.addEventListener("load", r), a.image;
                a = i[e] = i[e] || {};
                var o = a.image = new Image;
                o.addEventListener("load", r);
                var s = "data:",
                    l = e.substring(0, s.length).toLowerCase() === s;
                return l || (o.crossOrigin = t), o.src = e, o
            }, t.exports = n
        }, {}],
        60: [function (e, t, r) {
            "use strict";
            var n = e("../../../is"),
                i = e("../../../util"),
                a = function (e) {
                    this.init(e)
                },
                o = a,
                s = o.prototype;
            s.clientFunctions = ["redrawHint", "render", "renderTo", "matchCanvasSize", "nodeShapeImpl", "arrowShapeImpl"], s.init = function (e) {
                var t = this;
                t.options = e, t.cy = e.cy, t.container = e.cy.container(), t.selection = [void 0, void 0, void 0, void 0, 0], t.bezierProjPcts = [.05, .225, .4, .5, .6, .775, .95], t.hoverData = {
                    down: null,
                    last: null,
                    downTime: null,
                    triggerMode: null,
                    dragging: !1,
                    initialPan: [null, null],
                    capture: !1
                }, t.dragData = {
                    possibleDragElements: []
                }, t.touchData = {
                    start: null,
                    capture: !1,
                    startPosition: [null, null, null, null, null, null],
                    singleTouchStartTime: null,
                    singleTouchMoved: !0,
                    now: [null, null, null, null, null, null],
                    earlier: [null, null, null, null, null, null]
                }, t.redraws = 0, t.showFps = e.showFps, t.hideEdgesOnViewport = e.hideEdgesOnViewport, t.hideLabelsOnViewport = e.hideLabelsOnViewport, t.textureOnViewport = e.textureOnViewport, t.wheelSensitivity = e.wheelSensitivity, t.motionBlurEnabled = e.motionBlur, t.forcedPixelRatio = e.pixelRatio, t.motionBlur = e.motionBlur, t.motionBlurOpacity = e.motionBlurOpacity, t.motionBlurTransparency = 1 - t.motionBlurOpacity, t.motionBlurPxRatio = 1, t.mbPxRBlurry = 1, t.minMbLowQualFrames = 4, t.fullQualityMb = !1, t.clearedForMotionBlur = [], t.desktopTapThreshold = e.desktopTapThreshold, t.desktopTapThreshold2 = e.desktopTapThreshold * e.desktopTapThreshold, t.touchTapThreshold = e.touchTapThreshold, t.touchTapThreshold2 = e.touchTapThreshold * e.touchTapThreshold, t.tapholdDuration = 500, t.bindings = [], t.beforeRenderCallbacks = [], t.beforeRenderPriorities = {
                    animations: 400,
                    eleCalcs: 300,
                    eleTxrDeq: 200,
                    lyrTxrDeq: 100
                }, t.registerNodeShapes(), t.registerArrowShapes(), t.registerCalculationListeners(), t.load()
            }, s.notify = function (e) {
                var t, r = this;
                if (!this.destroyed) {
                    t = n.array(e.type) ? e.type : [e.type];
                    for (var i = {}, a = 0; a < t.length; a++) {
                        var o = t[a];
                        i[o] = !0
                    }
                    if (i.destroy) return void r.destroy();
                    (i.add || i.remove || i.load || i.style) && r.updateCachedZSortedEles(), i.viewport && r.redrawHint("select", !0), (i.load || i.resize) && (r.invalidateContainerClientCoordsCache(), r.matchCanvasSize(r.container)), r.redrawHint("eles", !0), r.redrawHint("drag", !0), this.startRenderLoop(), this.redraw()
                }
            }, s.destroy = function () {
                var e = this;
                e.destroyed = !0, e.cy.stopAnimationLoop();
                for (var t = 0; t < e.bindings.length; t++) {
                    var r = e.bindings[t],
                        n = r,
                        i = n.target;
                    (i.off || i.removeEventListener).apply(i, n.args)
                }
                if (e.bindings = [], e.beforeRenderCallbacks = [], e.onUpdateEleCalcsFns = [], e.removeObserver && e.removeObserver.disconnect(), e.styleObserver && e.styleObserver.disconnect(), e.labelCalcDiv) try {
                    document.body.removeChild(e.labelCalcDiv)
                } catch (a) { }
            }, [e("./arrow-shapes"), e("./coord-ele-math"), e("./images"), e("./load-listeners"), e("./node-shapes"), e("./redraw")].forEach(function (e) {
                i.extend(s, e)
            }), t.exports = o
        }, {
            "../../../is": 83,
            "../../../util": 100,
            "./arrow-shapes": 57,
            "./coord-ele-math": 58,
            "./images": 59,
            "./load-listeners": 61,
            "./node-shapes": 62,
            "./redraw": 63
        }],
        61: [function (e, t, r) {
            "use strict";
            var n = e("../../../is"),
                i = e("../../../util"),
                a = e("../../../math"),
                o = e("../../../event"),
                s = {};
            s.registerBinding = function (e, t, r, n) {
                var i = Array.prototype.slice.apply(arguments, [1]),
                    a = this.binder(e);
                return a.on.apply(a, i)
            }, s.binder = function (e) {
                var t = this,
                    r = e === window || e === document || e === document.body || n.domElement(e);
                if (null == t.supportsPassiveEvents) {
                    var i = !1;
                    try {
                        var a = Object.defineProperty({}, "passive", {
                            get: function () {
                                i = !0
                            }
                        });
                        window.addEventListener("test", null, a)
                    } catch (o) { }
                    t.supportsPassiveEvents = i
                }
                var s = function (n, i, a) {
                    var o = Array.prototype.slice.call(arguments);
                    return r && t.supportsPassiveEvents && (o[2] = {
                        capture: null != a ? a : !1,
                        passive: !1,
                        once: !1
                    }), t.bindings.push({
                        target: e,
                        args: o
                    }), (e.addEventListener || e.on).apply(e, o), this
                };
                return {
                    on: s,
                    addEventListener: s,
                    addListener: s,
                    bind: s
                }
            }, s.nodeIsDraggable = function (e) {
                return e && e.isNode() && !e.locked() && e.grabbable()
            }, s.nodeIsGrabbable = function (e) {
                return this.nodeIsDraggable(e) && 0 !== e.pstyle("opacity").value && "visible" === e.pstyle("visibility").value && "element" === e.pstyle("display").value
            }, s.load = function () {
                var e = this,
                    t = function (t, r, n, a) {
                        null == t && (t = e.cy);
                        for (var s = 0; s < r.length; s++) {
                            var l = r[s],
                                u = new o(n, i.extend({
                                    type: l
                                }, a));
                            t.trigger(u)
                        }
                    },
                    r = function (e) {
                        return e.shiftKey || e.metaKey || e.ctrlKey
                    },
                    s = function (t, r) {
                        var n = !0;
                        if (e.cy.hasCompoundNodes() && t && t.isEdge())
                            for (var i = 0; r && i < r.length; i++) {
                                var t = r[i];
                                if (t.isNode() && t.isParent()) {
                                    n = !1;
                                    break
                                }
                            } else n = !0;
                        return n
                    },
                    l = function (t) {
                        var r;
                        if (t.addToList && e.cy.hasCompoundNodes()) {
                            if (!t.addToList.hasId) {
                                t.addToList.hasId = {};
                                for (var n = 0; n < t.addToList.length; n++) {
                                    var i = t.addToList[n];
                                    t.addToList.hasId[i.id()] = !0
                                }
                            }
                            r = t.addToList.hasId
                        }
                        return r || {}
                    },
                    u = function (e) {
                        e[0]._private.grabbed = !0
                    },
                    c = function (e) {
                        e[0]._private.grabbed = !1
                    },
                    d = function (e) {
                        e[0]._private.rscratch.inDragLayer = !0
                    },
                    h = function (e) {
                        e[0]._private.rscratch.inDragLayer = !1
                    },
                    p = function (e) {
                        e[0]._private.rscratch.isGrabTarget = !0
                    },
                    f = function (e) {
                        e[0]._private.rscratch.isGrabTarget = !1
                    },
                    v = function (e, t) {
                        var r = l(t);
                        r[e.id()] || (t.addToList.push(e), r[e.id()] = !0, u(e))
                    },
                    g = function (e, t) {
                        if (e.cy().hasCompoundNodes() && (null != t.inDragLayer || null != t.addToList)) {
                            var r = e.descendants();
                            t.inDragLayer && (r.forEach(d), r.connectedEdges().forEach(d)), t.addToList && r.forEach(function (e) {
                                v(e, t)
                            })
                        }
                    },
                    y = function (t, r) {
                        r = r || {};
                        var n = t.cy().hasCompoundNodes();
                        r.inDragLayer && (t.forEach(d), t.neighborhood().stdFilter(function (e) {
                            return !n || e.isEdge()
                        }).forEach(d)), r.addToList && t.forEach(function (e) {
                            v(e, r)
                        }), g(t, r), x(t, {
                            inDragLayer: r.inDragLayer
                        }), e.updateCachedGrabbedEles()
                    },
                    m = y,
                    b = function (t) {
                        t && (t.hasId = {}, e.getCachedZSortedEles().forEach(function (e) {
                            c(e), h(e), f(e)
                        }), e.updateCachedGrabbedEles())
                    },
                    x = function (e, t) {
                        if ((null != t.inDragLayer || null != t.addToList) && e.cy().hasCompoundNodes()) {
                            var r = e.ancestors().orphans();
                            if (!r.same(e)) {
                                var n = r.descendants().spawnSelf().merge(r).unmerge(e).unmerge(e.descendants()),
                                    i = n.connectedEdges();
                                t.inDragLayer && (i.forEach(d), n.forEach(d)), t.addToList && n.forEach(function (e) {
                                    v(e, t)
                                })
                            }
                        }
                    },
                    w = "undefined" != typeof MutationObserver;
                w ? (e.removeObserver = new MutationObserver(function (t) {
                    for (var r = 0; r < t.length; r++) {
                        var n = t[r],
                            i = n.removedNodes;
                        if (i)
                            for (var a = 0; a < i.length; a++) {
                                var o = i[a];
                                if (o === e.container) {
                                    e.destroy();
                                    break
                                }
                            }
                    }
                }), e.container.parentNode && e.removeObserver.observe(e.container.parentNode, {
                    childList: !0
                })) : e.registerBinding(e.container, "DOMNodeRemoved", function (t) {
                    e.destroy()
                });
                var E = i.debounce(function () {
                    e.cy.invalidateSize(), e.invalidateContainerClientCoordsCache(), e.matchCanvasSize(e.container), e.redrawHint("eles", !0), e.redrawHint("drag", !0), e.redraw()
                }, 100);
                w && (e.styleObserver = new MutationObserver(E), e.styleObserver.observe(e.container, {
                    attributes: !0
                })), e.registerBinding(window, "resize", E);
                for (var _ = function (t) {
                    e.registerBinding(t, "scroll", function (t) {
                        e.invalidateContainerClientCoordsCache()
                    })
                }, S = e.cy.container(); _(S), S.parentNode;) S = S.parentNode;
                e.registerBinding(e.container, "contextmenu", function (e) {
                    e.preventDefault()
                });
                var P = function () {
                    return 0 !== e.selection[4]
                },
                    T = function (t) {
                        for (var r = e.findContainerClientCoords(), n = r[0], i = r[1], a = r[2], o = r[3], s = t.touches ? t.touches : [t], l = !1, u = 0; u < s.length; u++) {
                            var c = s[u];
                            if (n <= c.clientX && c.clientX <= n + a && i <= c.clientY && c.clientY <= i + o) {
                                l = !0;
                                break
                            }
                        }
                        if (!l) return !1;
                        for (var d = e.container, h = t.target, p = h.parentNode, f = !1; p;) {
                            if (p === d) {
                                f = !0;
                                break
                            }
                            p = p.parentNode
                        }
                        return !!f
                    };
                e.registerBinding(e.container, "mousedown", function (r) {
                    if (T(r)) {
                        r.preventDefault(), e.hoverData.capture = !0, e.hoverData.which = r.which;
                        var n = e.cy,
                            i = [r.clientX, r.clientY],
                            a = e.projectIntoViewport(i[0], i[1]),
                            s = e.selection,
                            l = e.findNearestElements(a[0], a[1], !0, !1),
                            u = l[0],
                            c = e.dragData.possibleDragElements;
                        e.hoverData.mdownPos = a, e.hoverData.mdownGPos = i;
                        var d = function () {
                            e.hoverData.tapholdCancelled = !1, clearTimeout(e.hoverData.tapholdTimeout), e.hoverData.tapholdTimeout = setTimeout(function () {
                                if (!e.hoverData.tapholdCancelled) {
                                    var t = e.hoverData.down;
                                    t ? t.trigger(new o(r, {
                                        type: "taphold",
                                        cyPosition: {
                                            x: a[0],
                                            y: a[1]
                                        }
                                    })) : n.trigger(new o(r, {
                                        type: "taphold",
                                        cyPosition: {
                                            x: a[0],
                                            y: a[1]
                                        }
                                    }))
                                }
                            }, e.tapholdDuration)
                        };
                        if (3 == r.which) {
                            e.hoverData.cxtStarted = !0;
                            var h = new o(r, {
                                type: "cxttapstart",
                                cyPosition: {
                                    x: a[0],
                                    y: a[1]
                                }
                            });
                            u ? (u.activate(), u.trigger(h), e.hoverData.down = u) : n.trigger(h), e.hoverData.downTime = (new Date).getTime(), e.hoverData.cxtDragged = !1
                        } else if (1 == r.which) {
                            if (u && u.activate(), null != u && e.nodeIsGrabbable(u)) {
                                var f = new o(r, {
                                    type: "grab",
                                    cyPosition: {
                                        x: a[0],
                                        y: a[1]
                                    }
                                });
                                if (p(u), u.selected()) {
                                    if (u.selected()) {
                                        c = e.dragData.possibleDragElements = [];
                                        var v = n.$(function () {
                                            return this.isNode() && this.selected() && e.nodeIsGrabbable(this)
                                        });
                                        y(v, {
                                            addToList: c
                                        }), u.trigger(f)
                                    }
                                } else c = e.dragData.possibleDragElements = [], m(u, {
                                    addToList: c
                                }), u.trigger(f);
                                e.redrawHint("eles", !0), e.redrawHint("drag", !0)
                            }
                            e.hoverData.down = u, e.hoverData.downs = l, e.hoverData.downTime = (new Date).getTime(), t(u, ["mousedown", "tapstart", "vmousedown"], r, {
                                cyPosition: {
                                    x: a[0],
                                    y: a[1]
                                }
                            }), null == u ? (s[4] = 1, e.data.bgActivePosistion = {
                                x: a[0],
                                y: a[1]
                            }, e.redrawHint("select", !0), e.redraw()) : u.isEdge() && (s[4] = 1), d()
                        }
                        s[0] = s[2] = a[0], s[1] = s[3] = a[1]
                    }
                }, !1), e.registerBinding(window, "mousemove", function (i) {
                    var l = e.hoverData.capture;
                    if (l || T(i)) {
                        var u = !1,
                            c = e.cy,
                            d = c.zoom(),
                            h = [i.clientX, i.clientY],
                            p = e.projectIntoViewport(h[0], h[1]),
                            f = e.hoverData.mdownPos,
                            v = e.hoverData.mdownGPos,
                            g = e.selection,
                            m = null;
                        e.hoverData.draggingEles || e.hoverData.dragging || e.hoverData.selecting || (m = e.findNearestElement(p[0], p[1], !0, !1));
                        var b, x = e.hoverData.last,
                            w = e.hoverData.down,
                            E = [p[0] - g[2], p[1] - g[3]],
                            _ = e.dragData.possibleDragElements;
                        if (v) {
                            var S = h[0] - v[0],
                                P = S * S,
                                k = h[1] - v[1],
                                D = k * k,
                                C = P + D;
                            b = C >= e.desktopTapThreshold2
                        }
                        var M = r(i);
                        b && (e.hoverData.tapholdCancelled = !0);
                        var N = function () {
                            var t = e.hoverData.dragDelta = e.hoverData.dragDelta || [];
                            0 === t.length ? (t.push(E[0]), t.push(E[1])) : (t[0] += E[0], t[1] += E[1])
                        };
                        if (u = !0, t(m, ["mousemove", "vmousemove", "tapdrag"], i, {
                            cyPosition: {
                                x: p[0],
                                y: p[1]
                            }
                        }), 3 === e.hoverData.which) {
                            if (b) {
                                var B = new o(i, {
                                    type: "cxtdrag",
                                    cyPosition: {
                                        x: p[0],
                                        y: p[1]
                                    }
                                });
                                w ? w.trigger(B) : c.trigger(B), e.hoverData.cxtDragged = !0, e.hoverData.cxtOver && m === e.hoverData.cxtOver || (e.hoverData.cxtOver && e.hoverData.cxtOver.trigger(new o(i, {
                                    type: "cxtdragout",
                                    cyPosition: {
                                        x: p[0],
                                        y: p[1]
                                    }
                                })), e.hoverData.cxtOver = m, m && m.trigger(new o(i, {
                                    type: "cxtdragover",
                                    cyPosition: {
                                        x: p[0],
                                        y: p[1]
                                    }
                                })))
                            }
                        } else if (e.hoverData.dragging) {
                            if (u = !0, c.panningEnabled() && c.userPanningEnabled()) {
                                var z;
                                if (e.hoverData.justStartedPan) {
                                    var I = e.hoverData.mdownPos;
                                    z = {
                                        x: (p[0] - I[0]) * d,
                                        y: (p[1] - I[1]) * d
                                    }, e.hoverData.justStartedPan = !1
                                } else z = {
                                    x: E[0] * d,
                                    y: E[1] * d
                                };
                                c.panBy(z), e.hoverData.dragged = !0
                            }
                            p = e.projectIntoViewport(i.clientX, i.clientY)
                        } else if (1 != g[4] || null != w && !w.isEdge()) {
                            if (w && w.isEdge() && w.active() && w.unactivate(), w && w.grabbed() || m == x || (x && t(x, ["mouseout", "tapdragout"], i, {
                                cyPosition: {
                                    x: p[0],
                                    y: p[1]
                                }
                            }), m && t(m, ["mouseover", "tapdragover"], i, {
                                cyPosition: {
                                    x: p[0],
                                    y: p[1]
                                }
                            }), e.hoverData.last = m), w && e.nodeIsDraggable(w))
                                if (b) {
                                    var L = !e.dragData.didDrag;
                                    L && e.redrawHint("eles", !0), e.dragData.didDrag = !0;
                                    var O = [];
                                    e.hoverData.draggingEles || y(c.collection(_), {
                                        inDragLayer: !0
                                    });
                                    for (var A = 0; A < _.length; A++) {
                                        var R = _[A];
                                        if (e.nodeIsDraggable(R) && R.grabbed()) {
                                            var q = R._private.position;
                                            if (O.push(R), n.number(E[0]) && n.number(E[1])) {
                                                var V = !R.isParent();
                                                if (V && (q.x += E[0], q.y += E[1]), L) {
                                                    var F = e.hoverData.dragDelta;
                                                    V && F && n.number(F[0]) && n.number(F[1]) && (q.x += F[0], q.y += F[1])
                                                }
                                            }
                                        }
                                    }
                                    e.hoverData.draggingEles = !0;
                                    var j = c.collection(O);
                                    j.updateCompoundBounds(), j.trigger("position drag"), e.redrawHint("drag", !0), e.redraw()
                                } else N();
                            u = !0
                        } else if (b) {
                            if (e.hoverData.dragging || !c.boxSelectionEnabled() || !M && c.panningEnabled() && c.userPanningEnabled()) {
                                if (!e.hoverData.selecting && c.panningEnabled() && c.userPanningEnabled()) {
                                    var X = s(w, e.hoverData.downs);
                                    X && (e.hoverData.dragging = !0, e.hoverData.justStartedPan = !0, g[4] = 0, e.data.bgActivePosistion = a.array2point(f), e.redrawHint("select", !0), e.redraw())
                                }
                            } else e.data.bgActivePosistion = void 0, e.hoverData.selecting || c.trigger("boxstart"), e.hoverData.selecting = !0, e.redrawHint("select", !0), e.redraw();
                            w && w.isEdge() && w.active() && w.unactivate()
                        }
                        return g[2] = p[0], g[3] = p[1], u ? (i.stopPropagation && i.stopPropagation(), i.preventDefault && i.preventDefault(), !1) : void 0
                    }
                }, !1), e.registerBinding(window, "mouseup", function (n) {
                    var i = e.hoverData.capture;
                    if (i) {
                        e.hoverData.capture = !1;
                        var a = e.cy,
                            s = e.projectIntoViewport(n.clientX, n.clientY),
                            l = e.selection,
                            u = e.findNearestElement(s[0], s[1], !0, !1),
                            c = e.dragData.possibleDragElements,
                            d = e.hoverData.down,
                            h = r(n);
                        if (e.data.bgActivePosistion && (e.redrawHint("select", !0), e.redraw()), e.hoverData.tapholdCancelled = !0, e.data.bgActivePosistion = void 0, d && d.unactivate(), 3 === e.hoverData.which) {
                            var p = new o(n, {
                                type: "cxttapend",
                                cyPosition: {
                                    x: s[0],
                                    y: s[1]
                                }
                            });
                            if (d ? d.trigger(p) : a.trigger(p), !e.hoverData.cxtDragged) {
                                var f = new o(n, {
                                    type: "cxttap",
                                    cyPosition: {
                                        x: s[0],
                                        y: s[1]
                                    }
                                });
                                d ? d.trigger(f) : a.trigger(f)
                            }
                            e.hoverData.cxtDragged = !1, e.hoverData.which = null
                        } else if (1 === e.hoverData.which) {
                            if (null != d || e.dragData.didDrag || e.hoverData.selecting || e.hoverData.dragged || r(n) || (a.$(function () {
                                return this.selected()
                            }).unselect(), c.length > 0 && e.redrawHint("eles", !0), e.dragData.possibleDragElements = c = []), t(u, ["mouseup", "tapend", "vmouseup"], n, {
                                cyPosition: {
                                    x: s[0],
                                    y: s[1]
                                }
                            }), e.dragData.didDrag || e.hoverData.dragged || e.hoverData.selecting || t(d, ["click", "tap", "vclick"], n, {
                                cyPosition: {
                                    x: s[0],
                                    y: s[1]
                                }
                            }), u != d || e.dragData.didDrag || e.hoverData.selecting || null != u && u._private.selectable && (e.hoverData.dragging || ("additive" === a.selectionType() || h ? u.selected() ? u.unselect() : u.select() : h || (a.$(":selected").unmerge(u).unselect(), u.select())), e.redrawHint("eles", !0)), e.hoverData.selecting) {
                                var v = a.collection(e.getAllInBox(l[0], l[1], l[2], l[3]));
                                e.redrawHint("select", !0), v.length > 0 && e.redrawHint("eles", !0), a.trigger("boxend");
                                var g = function (e) {
                                    return e.selectable() && !e.selected()
                                };
                                "additive" === a.selectionType() ? v.trigger("box").stdFilter(g).select().trigger("boxselect") : (h || a.$(":selected").unmerge(v).unselect(), v.trigger("box").stdFilter(g).select().trigger("boxselect")), e.redraw()
                            }
                            if (e.hoverData.dragging && (e.hoverData.dragging = !1, e.redrawHint("select", !0), e.redrawHint("eles", !0), e.redraw()), !l[4]) {
                                e.redrawHint("drag", !0), e.redrawHint("eles", !0);
                                var y = d && d.grabbed();
                                b(c), y && d.trigger("free")
                            }
                        }
                        l[4] = 0, e.hoverData.down = null, e.hoverData.cxtStarted = !1, e.hoverData.draggingEles = !1, e.hoverData.selecting = !1, e.dragData.didDrag = !1, e.hoverData.dragged = !1, e.hoverData.dragDelta = [], e.hoverData.mdownPos = null, e.hoverData.mdownGPos = null
                    }
                }, !1);
                var k = function (t) {
                    if (!e.scrollingPage) {
                        var r = e.cy,
                            n = e.projectIntoViewport(t.clientX, t.clientY),
                            i = [n[0] * r.zoom() + r.pan().x, n[1] * r.zoom() + r.pan().y];
                        if (e.hoverData.draggingEles || e.hoverData.dragging || e.hoverData.cxtStarted || P()) return void t.preventDefault();
                        if (r.panningEnabled() && r.userPanningEnabled() && r.zoomingEnabled() && r.userZoomingEnabled()) {
                            t.preventDefault(), e.data.wheelZooming = !0, clearTimeout(e.data.wheelTimeout), e.data.wheelTimeout = setTimeout(function () {
                                e.data.wheelZooming = !1, e.redrawHint("eles", !0), e.redraw()
                            }, 150);
                            var a;
                            a = null != t.deltaY ? t.deltaY / -250 : null != t.wheelDeltaY ? t.wheelDeltaY / 1e3 : t.wheelDelta / 1e3, a *= e.wheelSensitivity;
                            var o = 1 === t.deltaMode;
                            o && (a *= 33), r.zoom({
                                level: r.zoom() * Math.pow(10, a),
                                renderedPosition: {
                                    x: i[0],
                                    y: i[1]
                                }
                            })
                        }
                    }
                };
                e.registerBinding(e.container, "wheel", k, !0), e.registerBinding(window, "scroll", function (t) {
                    e.scrollingPage = !0, clearTimeout(e.scrollingPageTimeout), e.scrollingPageTimeout = setTimeout(function () {
                        e.scrollingPage = !1
                    }, 250)
                }, !0), e.registerBinding(e.container, "mouseout", function (t) {
                    var r = e.projectIntoViewport(t.clientX, t.clientY);
                    e.cy.trigger(new o(t, {
                        type: "mouseout",
                        cyPosition: {
                            x: r[0],
                            y: r[1]
                        }
                    }))
                }, !1), e.registerBinding(e.container, "mouseover", function (t) {
                    var r = e.projectIntoViewport(t.clientX, t.clientY);
                    e.cy.trigger(new o(t, {
                        type: "mouseover",
                        cyPosition: {
                            x: r[0],
                            y: r[1]
                        }
                    }))
                }, !1);
                var D, C, M, N, B, z, I, L, O, A, R, q, V, F, j = function (e, t, r, n) {
                    return Math.sqrt((r - e) * (r - e) + (n - t) * (n - t))
                },
                    X = function (e, t, r, n) {
                        return (r - e) * (r - e) + (n - t) * (n - t)
                    };
                e.registerBinding(e.container, "touchstart", F = function (r) {
                    if (T(r)) {
                        e.touchData.capture = !0, e.data.bgActivePosistion = void 0;
                        var n = e.cy,
                            i = e.touchData.now,
                            a = e.touchData.earlier;
                        if (r.touches[0]) {
                            var s = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
                            i[0] = s[0], i[1] = s[1]
                        }
                        if (r.touches[1]) {
                            var s = e.projectIntoViewport(r.touches[1].clientX, r.touches[1].clientY);
                            i[2] = s[0], i[3] = s[1]
                        }
                        if (r.touches[2]) {
                            var s = e.projectIntoViewport(r.touches[2].clientX, r.touches[2].clientY);
                            i[4] = s[0], i[5] = s[1]
                        }
                        if (r.touches[1]) {
                            b(e.dragData.touchDragEles);
                            var l = e.findContainerClientCoords();
                            O = l[0], A = l[1], R = l[2], q = l[3], D = r.touches[0].clientX - O, C = r.touches[0].clientY - A, M = r.touches[1].clientX - O, N = r.touches[1].clientY - A, V = D >= 0 && R >= D && M >= 0 && R >= M && C >= 0 && q >= C && N >= 0 && q >= N;
                            var u = n.pan(),
                                c = n.zoom();
                            B = j(D, C, M, N), z = X(D, C, M, N), I = [(D + M) / 2, (C + N) / 2], L = [(I[0] - u.x) / c, (I[1] - u.y) / c];
                            var d = 200,
                                h = d * d;
                            if (h > z && !r.touches[2]) {
                                var f = e.findNearestElement(i[0], i[1], !0, !0),
                                    v = e.findNearestElement(i[2], i[3], !0, !0);
                                return f && f.isNode() ? (f.activate().trigger(new o(r, {
                                    type: "cxttapstart",
                                    cyPosition: {
                                        x: i[0],
                                        y: i[1]
                                    }
                                })), e.touchData.start = f) : v && v.isNode() ? (v.activate().trigger(new o(r, {
                                    type: "cxttapstart",
                                    cyPosition: {
                                        x: i[0],
                                        y: i[1]
                                    }
                                })), e.touchData.start = v) : n.trigger(new o(r, {
                                    type: "cxttapstart",
                                    cyPosition: {
                                        x: i[0],
                                        y: i[1]
                                    }
                                })), e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxt = !0, e.touchData.cxtDragged = !1, e.data.bgActivePosistion = void 0, void e.redraw()
                            }
                        }
                        if (r.touches[2]);
                        else if (r.touches[1]);
                        else if (r.touches[0]) {
                            var g = e.findNearestElements(i[0], i[1], !0, !0),
                                x = g[0];
                            if (null != x && (x.activate(), e.touchData.start = x, e.touchData.starts = g, e.nodeIsGrabbable(x))) {
                                var w = e.dragData.touchDragEles = [];
                                if (e.redrawHint("eles", !0), e.redrawHint("drag", !0), x.selected()) {
                                    var E = n.$(function () {
                                        return this.selected() && e.nodeIsGrabbable(this)
                                    });
                                    y(E, {
                                        addToList: w
                                    })
                                } else m(x, {
                                    addToList: w
                                });
                                p(x), x.trigger(new o(r, {
                                    type: "grab",
                                    cyPosition: {
                                        x: i[0],
                                        y: i[1]
                                    }
                                }))
                            }
                            t(x, ["touchstart", "tapstart", "vmousedown"], r, {
                                cyPosition: {
                                    x: i[0],
                                    y: i[1]
                                }
                            }), null == x && (e.data.bgActivePosistion = {
                                x: s[0],
                                y: s[1]
                            }, e.redrawHint("select", !0), e.redraw()), e.touchData.singleTouchMoved = !1, e.touchData.singleTouchStartTime = +new Date, clearTimeout(e.touchData.tapholdTimeout), e.touchData.tapholdTimeout = setTimeout(function () {
                                e.touchData.singleTouchMoved !== !1 || e.pinching || e.touchData.selecting || (t(e.touchData.start, ["taphold"], r, {
                                    cyPosition: {
                                        x: i[0],
                                        y: i[1]
                                    }
                                }), e.touchData.start || n.$(":selected").unselect())
                            }, e.tapholdDuration)
                        }
                        if (r.touches.length >= 1) {
                            for (var _ = e.touchData.startPosition = [], S = 0; S < i.length; S++) _[S] = a[S] = i[S];
                            var P = r.touches[0];
                            e.touchData.startGPosition = [P.clientX, P.clientY]
                        }
                    }
                }, !1);
                var Y;
                e.registerBinding(window, "touchmove", Y = function (r) {
                    var i = e.touchData.capture;
                    if (i || T(r)) {
                        var l = e.selection,
                            u = e.cy,
                            c = e.touchData.now,
                            d = e.touchData.earlier,
                            h = u.zoom();
                        if (r.touches[0]) {
                            var p = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
                            c[0] = p[0], c[1] = p[1]
                        }
                        if (r.touches[1]) {
                            var p = e.projectIntoViewport(r.touches[1].clientX, r.touches[1].clientY);
                            c[2] = p[0], c[3] = p[1]
                        }
                        if (r.touches[2]) {
                            var p = e.projectIntoViewport(r.touches[2].clientX, r.touches[2].clientY);
                            c[4] = p[0], c[5] = p[1]
                        }
                        var f, v = e.touchData.startGPosition;
                        if (i && r.touches[0] && v) {
                            for (var g = [], m = 0; m < c.length; m++) g[m] = c[m] - d[m];
                            var x = r.touches[0].clientX - v[0],
                                w = x * x,
                                E = r.touches[0].clientY - v[1],
                                _ = E * E,
                                S = w + _;
                            f = S >= e.touchTapThreshold2
                        }
                        if (i && e.touchData.cxt) {
                            r.preventDefault();
                            var P = r.touches[0].clientX - O,
                                k = r.touches[0].clientY - A,
                                I = r.touches[1].clientX - O,
                                R = r.touches[1].clientY - A,
                                q = X(P, k, I, R),
                                F = q / z,
                                Y = 150,
                                W = Y * Y,
                                $ = 1.5,
                                H = $ * $;
                            if (F >= H || q >= W) {
                                e.touchData.cxt = !1, e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
                                var U = new o(r, {
                                    type: "cxttapend",
                                    cyPosition: {
                                        x: c[0],
                                        y: c[1]
                                    }
                                });
                                e.touchData.start ? (e.touchData.start.unactivate().trigger(U), e.touchData.start = null) : u.trigger(U)
                            }
                        }
                        if (i && e.touchData.cxt) {
                            var U = new o(r, {
                                type: "cxtdrag",
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            });
                            e.data.bgActivePosistion = void 0, e.redrawHint("select", !0), e.touchData.start ? e.touchData.start.trigger(U) : u.trigger(U), e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxtDragged = !0;
                            var Z = e.findNearestElement(c[0], c[1], !0, !0);
                            e.touchData.cxtOver && Z === e.touchData.cxtOver || (e.touchData.cxtOver && e.touchData.cxtOver.trigger(new o(r, {
                                type: "cxtdragout",
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            })), e.touchData.cxtOver = Z, Z && Z.trigger(new o(r, {
                                type: "cxtdragover",
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            })))
                        } else if (i && r.touches[2] && u.boxSelectionEnabled()) r.preventDefault(), e.data.bgActivePosistion = void 0, this.lastThreeTouch = +new Date, e.touchData.selecting || u.trigger("boxstart"), e.touchData.selecting = !0, e.redrawHint("select", !0), l && 0 !== l.length && void 0 !== l[0] ? (l[2] = (c[0] + c[2] + c[4]) / 3, l[3] = (c[1] + c[3] + c[5]) / 3) : (l[0] = (c[0] + c[2] + c[4]) / 3, l[1] = (c[1] + c[3] + c[5]) / 3, l[2] = (c[0] + c[2] + c[4]) / 3 + 1, l[3] = (c[1] + c[3] + c[5]) / 3 + 1), l[4] = 1, e.touchData.selecting = !0, e.redraw();
                        else if (i && r.touches[1] && u.zoomingEnabled() && u.panningEnabled() && u.userZoomingEnabled() && u.userPanningEnabled()) {
                            r.preventDefault(), e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
                            var G = e.dragData.touchDragEles;
                            if (G) {
                                e.redrawHint("drag", !0);
                                for (var Q = 0; Q < G.length; Q++) {
                                    var K = G[Q]._private;
                                    K.grabbed = !1, K.rscratch.inDragLayer = !1
                                }
                            }
                            var P = r.touches[0].clientX - O,
                                k = r.touches[0].clientY - A,
                                I = r.touches[1].clientX - O,
                                R = r.touches[1].clientY - A,
                                J = j(P, k, I, R),
                                ee = J / B;
                            if (V) {
                                var te = P - D,
                                    re = k - C,
                                    ne = I - M,
                                    ie = R - N,
                                    ae = (te + ne) / 2,
                                    oe = (re + ie) / 2,
                                    se = u.zoom(),
                                    le = se * ee,
                                    ue = u.pan(),
                                    ce = L[0] * se + ue.x,
                                    de = L[1] * se + ue.y,
                                    he = {
                                        x: -le / se * (ce - ue.x - ae) + ce,
                                        y: -le / se * (de - ue.y - oe) + de
                                    };
                                if (e.touchData.start && e.touchData.start.active()) {
                                    var G = e.dragData.touchDragEles;
                                    b(G), e.redrawHint("drag", !0), e.redrawHint("eles", !0), e.touchData.start.unactivate().trigger("free")
                                }
                                u.viewport({
                                    zoom: le,
                                    pan: he,
                                    cancelOnFailedZoom: !0
                                }), B = J, D = P, C = k, M = I, N = R, e.pinching = !0
                            }
                            if (r.touches[0]) {
                                var p = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
                                c[0] = p[0], c[1] = p[1]
                            }
                            if (r.touches[1]) {
                                var p = e.projectIntoViewport(r.touches[1].clientX, r.touches[1].clientY);
                                c[2] = p[0], c[3] = p[1]
                            }
                            if (r.touches[2]) {
                                var p = e.projectIntoViewport(r.touches[2].clientX, r.touches[2].clientY);
                                c[4] = p[0], c[5] = p[1]
                            }
                        } else if (r.touches[0]) {
                            var Z, pe = e.touchData.start,
                                fe = e.touchData.last;
                            if (e.hoverData.draggingEles || e.swipePanning || (Z = e.findNearestElement(c[0], c[1], !0, !0)), i && null != pe && r.preventDefault(), i && null != pe && e.nodeIsDraggable(pe))
                                if (f) {
                                    var G = e.dragData.touchDragEles,
                                        ve = !e.dragData.didDrag;
                                    ve && y(u.collection(G), {
                                        inDragLayer: !0
                                    });
                                    for (var ge = 0; ge < G.length; ge++) {
                                        var ye = G[ge];
                                        if (e.nodeIsDraggable(ye) && ye.grabbed()) {
                                            e.dragData.didDrag = !0;
                                            var me = ye._private.position,
                                                be = !ye.isParent();
                                            if (be && n.number(g[0]) && n.number(g[1]) && (me.x += g[0], me.y += g[1]), ve) {
                                                e.redrawHint("eles", !0);
                                                var xe = e.touchData.dragDelta;
                                                be && xe && n.number(xe[0]) && n.number(xe[1]) && (me.x += xe[0], me.y += xe[1])
                                            }
                                        }
                                    }
                                    var we = u.collection(G);
                                    we.updateCompoundBounds(), we.trigger("position drag"), e.hoverData.draggingEles = !0, e.redrawHint("drag", !0), e.touchData.startPosition[0] == d[0] && e.touchData.startPosition[1] == d[1] && e.redrawHint("eles", !0), e.redraw()
                                } else {
                                    var xe = e.touchData.dragDelta = e.touchData.dragDelta || [];
                                    0 === xe.length ? (xe.push(g[0]), xe.push(g[1])) : (xe[0] += g[0], xe[1] += g[1])
                                }
                            if (t(pe || Z, ["touchmove", "tapdrag", "vmousemove"], r, {
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            }), pe && pe.grabbed() || Z == fe || (fe && fe.trigger(new o(r, {
                                type: "tapdragout",
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            })), Z && Z.trigger(new o(r, {
                                type: "tapdragover",
                                cyPosition: {
                                    x: c[0],
                                    y: c[1]
                                }
                            }))), e.touchData.last = Z, i)
                                for (var Q = 0; Q < c.length; Q++) c[Q] && e.touchData.startPosition[Q] && f && (e.touchData.singleTouchMoved = !0);
                            if (i && (null == pe || pe.isEdge()) && u.panningEnabled() && u.userPanningEnabled()) {
                                var Ee = s(pe, e.touchData.starts);
                                Ee && (r.preventDefault(), e.swipePanning ? u.panBy({
                                    x: g[0] * h,
                                    y: g[1] * h
                                }) : f && (e.swipePanning = !0, u.panBy({
                                    x: x * h,
                                    y: E * h
                                }), pe && (pe.unactivate(), e.data.bgActivePosistion || (e.data.bgActivePosistion = a.array2point(e.touchData.startPosition)), e.redrawHint("select", !0), e.touchData.start = null)));
                                var p = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
                                c[0] = p[0], c[1] = p[1]
                            }
                        }
                        for (var m = 0; m < c.length; m++) d[m] = c[m]
                    }
                }, !1);
                var W;
                e.registerBinding(window, "touchcancel", W = function (t) {
                    var r = e.touchData.start;
                    e.touchData.capture = !1, r && r.unactivate()
                });
                var $;
                if (e.registerBinding(window, "touchend", $ = function (r) {
                    var n = e.touchData.start,
                        i = e.touchData.capture;
                    if (i) {
                        e.touchData.capture = !1, r.preventDefault();
                        var a = e.selection;
                        e.swipePanning = !1, e.hoverData.draggingEles = !1;
                        var s = e.cy,
                            l = s.zoom(),
                            u = e.touchData.now,
                            c = e.touchData.earlier;
                        if (r.touches[0]) {
                            var d = e.projectIntoViewport(r.touches[0].clientX, r.touches[0].clientY);
                            u[0] = d[0], u[1] = d[1]
                        }
                        if (r.touches[1]) {
                            var d = e.projectIntoViewport(r.touches[1].clientX, r.touches[1].clientY);
                            u[2] = d[0], u[3] = d[1]
                        }
                        if (r.touches[2]) {
                            var d = e.projectIntoViewport(r.touches[2].clientX, r.touches[2].clientY);
                            u[4] = d[0], u[5] = d[1]
                        }
                        n && n.unactivate();
                        var h;
                        if (e.touchData.cxt) {
                            if (h = new o(r, {
                                type: "cxttapend",
                                cyPosition: {
                                    x: u[0],
                                    y: u[1]
                                }
                            }), n ? n.trigger(h) : s.trigger(h), !e.touchData.cxtDragged) {
                                var p = new o(r, {
                                    type: "cxttap",
                                    cyPosition: {
                                        x: u[0],
                                        y: u[1]
                                    }
                                });
                                n ? n.trigger(p) : s.trigger(p)
                            }
                            return e.touchData.start && (e.touchData.start._private.grabbed = !1), e.touchData.cxt = !1, e.touchData.start = null, void e.redraw()
                        }
                        if (!r.touches[2] && s.boxSelectionEnabled() && e.touchData.selecting) {
                            e.touchData.selecting = !1;
                            var f = s.collection(e.getAllInBox(a[0], a[1], a[2], a[3]));
                            a[0] = void 0, a[1] = void 0, a[2] = void 0, a[3] = void 0, a[4] = 0, e.redrawHint("select", !0), s.trigger("boxend");
                            var v = function (e) {
                                return e.selectable() && !e.selected()
                            };
                            f.trigger("box").stdFilter(v).select().trigger("boxselect"), f.nonempty() && e.redrawHint("eles", !0), e.redraw()
                        }
                        if (null != n && n.unactivate(), r.touches[2]) e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
                        else if (r.touches[1]);
                        else if (r.touches[0]);
                        else if (!r.touches[0]) {
                            e.data.bgActivePosistion = void 0, e.redrawHint("select", !0);
                            var g = e.dragData.touchDragEles;
                            if (null != n) {
                                var y = n._private.grabbed;
                                b(g), e.redrawHint("drag", !0), e.redrawHint("eles", !0), y && n.trigger("free"), t(n, ["touchend", "tapend", "vmouseup", "tapdragout"], r, {
                                    cyPosition: {
                                        x: u[0],
                                        y: u[1]
                                    }
                                }), n.unactivate(), e.touchData.start = null
                            } else {
                                var m = e.findNearestElement(u[0], u[1], !0, !0);
                                t(m, ["touchend", "tapend", "vmouseup", "tapdragout"], r, {
                                    cyPosition: {
                                        x: u[0],
                                        y: u[1]
                                    }
                                })
                            }
                            var x = e.touchData.startPosition[0] - u[0],
                                w = x * x,
                                E = e.touchData.startPosition[1] - u[1],
                                _ = E * E,
                                S = w + _,
                                P = S * l * l;
                            null != n && !e.dragData.didDrag && n._private.selectable && P < e.touchTapThreshold2 && !e.pinching && ("single" === s.selectionType() ? (s.$(":selected").unmerge(n).unselect(), n.select()) : n.selected() ? n.unselect() : n.select(), e.redrawHint("eles", !0)), e.touchData.singleTouchMoved || t(n, ["tap", "vclick"], r, {
                                cyPosition: {
                                    x: u[0],
                                    y: u[1]
                                }
                            }), e.touchData.singleTouchMoved = !0
                        }
                        for (var T = 0; T < u.length; T++) c[T] = u[T];
                        e.dragData.didDrag = !1, 0 === r.touches.length && (e.touchData.dragDelta = [], e.touchData.startPosition = null, e.touchData.startGPosition = null), r.touches.length < 2 && (e.pinching = !1, e.redrawHint("eles", !0), e.redraw())
                    }
                }, !1), "undefined" == typeof TouchEvent) {
                    var H = [],
                        U = function (e) {
                            return {
                                clientX: e.clientX,
                                clientY: e.clientY,
                                force: 1,
                                identifier: e.pointerId,
                                pageX: e.pageX,
                                pageY: e.pageY,
                                radiusX: e.width / 2,
                                radiusY: e.height / 2,
                                screenX: e.screenX,
                                screenY: e.screenY,
                                target: e.target
                            }
                        },
                        Z = function (e) {
                            return {
                                event: e,
                                touch: U(e)
                            }
                        },
                        G = function (e) {
                            H.push(Z(e))
                        },
                        Q = function (e) {
                            for (var t = 0; t < H.length; t++) {
                                var r = H[t];
                                if (r.event.pointerId === e.pointerId) return void H.splice(t, 1)
                            }
                        },
                        K = function (e) {
                            var t = H.filter(function (t) {
                                return t.event.pointerId === e.pointerId
                            })[0];
                            t.event = e, t.touch = U(e)
                        },
                        J = function (e) {
                            e.touches = H.map(function (e) {
                                return e.touch
                            })
                        };
                    e.registerBinding(e.container, "pointerdown", function (e) {
                        "mouse" !== e.pointerType && (e.preventDefault(), G(e), J(e), F(e))
                    }), e.registerBinding(e.container, "pointerup", function (e) {
                        "mouse" !== e.pointerType && (Q(e), J(e), $(e))
                    }), e.registerBinding(e.container, "pointercancel", function (e) {
                        "mouse" !== e.pointerType && (Q(e), J(e), W(e))
                    }), e.registerBinding(e.container, "pointermove", function (e) {
                        "mouse" !== e.pointerType && (e.preventDefault(), K(e), J(e), Y(e))
                    })
                }
            }, t.exports = s
        }, {
            "../../../event": 45,
            "../../../is": 83,
            "../../../math": 85,
            "../../../util": 100
        }],
        62: [function (e, t, r) {
            "use strict";
            var n = e("../../../math"),
                i = {};
            i.generatePolygon = function (e, t) {
                return this.nodeShapes[e] = {
                    renderer: this,
                    name: e,
                    points: t,
                    draw: function (e, t, r, n, i) {
                        this.renderer.nodeShapeImpl("polygon", e, t, r, n, i, this.points)
                    },
                    intersectLine: function (e, t, r, i, a, o, s) {
                        return n.polygonIntersectLine(a, o, this.points, e, t, r / 2, i / 2, s)
                    },
                    checkPoint: function (e, t, r, i, a, o, s) {
                        return n.pointInsidePolygon(e, t, this.points, o, s, i, a, [0, -1], r)
                    }
                }
            }, i.generateEllipse = function () {
                return this.nodeShapes.ellipse = {
                    renderer: this,
                    name: "ellipse",
                    draw: function (e, t, r, n, i) {
                        this.renderer.nodeShapeImpl(this.name, e, t, r, n, i)
                    },
                    intersectLine: function (e, t, r, i, a, o, s) {
                        return n.intersectLineEllipse(a, o, e, t, r / 2 + s, i / 2 + s)
                    },
                    checkPoint: function (e, t, r, n, i, a, o) {
                        return e -= a, t -= o, e /= n / 2 + r, t /= i / 2 + r, 1 >= e * e + t * t
                    }
                }
            }, i.generateRoundRectangle = function () {
                return this.nodeShapes.roundrectangle = {
                    renderer: this,
                    name: "roundrectangle",
                    points: n.generateUnitNgonPointsFitToSquare(4, 0),
                    draw: function (e, t, r, n, i) {
                        this.renderer.nodeShapeImpl(this.name, e, t, r, n, i)
                    },
                    intersectLine: function (e, t, r, i, a, o, s) {
                        return n.roundRectangleIntersectLine(a, o, e, t, r, i, s)
                    },
                    checkPoint: function (e, t, r, i, a, o, s) {
                        var l = n.getRoundRectangleRadius(i, a);
                        if (n.pointInsidePolygon(e, t, this.points, o, s, i, a - 2 * l, [0, -1], r)) return !0;
                        if (n.pointInsidePolygon(e, t, this.points, o, s, i - 2 * l, a, [0, -1], r)) return !0;
                        var u = function (e, t, r, n, i, a, o) {
                            return e -= r, t -= n, e /= i / 2 + o, t /= a / 2 + o, 1 >= e * e + t * t
                        };
                        return u(e, t, o - i / 2 + l, s - a / 2 + l, 2 * l, 2 * l, r) ? !0 : u(e, t, o + i / 2 - l, s - a / 2 + l, 2 * l, 2 * l, r) ? !0 : u(e, t, o + i / 2 - l, s + a / 2 - l, 2 * l, 2 * l, r) ? !0 : !!u(e, t, o - i / 2 + l, s + a / 2 - l, 2 * l, 2 * l, r)
                    }
                }
            }, i.registerNodeShapes = function () {
                var e = this.nodeShapes = {},
                    t = this;
                this.generateEllipse(), this.generatePolygon("triangle", n.generateUnitNgonPointsFitToSquare(3, 0)), this.generatePolygon("rectangle", n.generateUnitNgonPointsFitToSquare(4, 0)), e.square = e.rectangle, this.generateRoundRectangle(), this.generatePolygon("diamond", [0, 1, 1, 0, 0, -1, -1, 0]), this.generatePolygon("pentagon", n.generateUnitNgonPointsFitToSquare(5, 0)), this.generatePolygon("hexagon", n.generateUnitNgonPointsFitToSquare(6, 0)), this.generatePolygon("heptagon", n.generateUnitNgonPointsFitToSquare(7, 0)), this.generatePolygon("octagon", n.generateUnitNgonPointsFitToSquare(8, 0));
                var r = new Array(20),
                    i = n.generateUnitNgonPoints(5, 0),
                    a = n.generateUnitNgonPoints(5, Math.PI / 5),
                    o = .5 * (3 - Math.sqrt(5));
                o *= 1.57;
                for (var s = 0; s < a.length / 2; s++) a[2 * s] *= o, a[2 * s + 1] *= o;
                for (var s = 0; 5 > s; s++) r[4 * s] = i[2 * s], r[4 * s + 1] = i[2 * s + 1], r[4 * s + 2] = a[2 * s], r[4 * s + 3] = a[2 * s + 1];
                r = n.fitPolygonToSquare(r), this.generatePolygon("star", r), this.generatePolygon("vee", [-1, -1, 0, -.333, 1, -1, 0, 1]), this.generatePolygon("rhomboid", [-1, -1, .333, -1, 1, 1, -.333, 1]), e.makePolygon = function (e) {
                    var r, n = e.join("$"),
                        i = "polygon-" + n;
                    return (r = this[i]) ? r : t.generatePolygon(i, e)
                }
            }, t.exports = i
        }, {
            "../../../math": 85
        }],
        63: [function (e, t, r) {
            "use strict";
            var n = e("../../../util"),
                i = {};
            i.timeToRender = function () {
                return this.redrawTotalTime / this.redrawCount
            }, i.redraw = function (e) {
                e = e || n.staticEmptyObject();
                var t = this;
                void 0 === t.averageRedrawTime && (t.averageRedrawTime = 0), void 0 === t.lastRedrawTime && (t.lastRedrawTime = 0), void 0 === t.lastDrawTime && (t.lastDrawTime = 0), t.requestedFrame = !0, t.renderOptions = e
            }, i.beforeRender = function (e, t) {
                if (!this.destroyed) {
                    t = t || 0;
                    var r = this.beforeRenderCallbacks;
                    r.push({
                        fn: e,
                        priority: t
                    }), r.sort(function (e, t) {
                        return t.priority - e.priority
                    })
                }
            };
            var a = function (e, t, r) {
                for (var n = e.beforeRenderCallbacks, i = 0; i < n.length; i++) n[i].fn(t, r)
            };
            i.startRenderLoop = function () {
                var e = this;
                if (!e.renderLoopStarted) {
                    e.renderLoopStarted = !0;
                    var t = function (r) {
                        if (!e.destroyed) {
                            if (e.requestedFrame && !e.skipFrame) {
                                a(e, !0, r);
                                var i = n.performanceNow();
                                e.render(e.renderOptions);
                                var o = e.lastDrawTime = n.performanceNow();
                                void 0 === e.averageRedrawTime && (e.averageRedrawTime = o - i), void 0 === e.redrawCount && (e.redrawCount = 0), e.redrawCount++ , void 0 === e.redrawTotalTime && (e.redrawTotalTime = 0);
                                var s = o - i;
                                e.redrawTotalTime += s, e.lastRedrawTime = s, e.averageRedrawTime = e.averageRedrawTime / 2 + s / 2, e.requestedFrame = !1
                            } else a(e, !1, r);
                            e.skipFrame = !1, n.requestAnimationFrame(t)
                        }
                    };
                    n.requestAnimationFrame(t)
                }
            }, t.exports = i
        }, {
            "../../../util": 100
        }],
        64: [function (e, t, r) {
            "use strict";
            var n, i = {};
            i.arrowShapeImpl = function (e) {
                return (n || (n = {
                    polygon: function (e, t) {
                        for (var r = 0; r < t.length; r++) {
                            var n = t[r];
                            e.lineTo(n.x, n.y)
                        }
                    },
                    "triangle-backcurve": function (e, t, r) {
                        for (var n, i = 0; i < t.length; i++) {
                            var a = t[i];
                            0 === i && (n = a), e.lineTo(a.x, a.y)
                        }
                        e.quadraticCurveTo(r.x, r.y, n.x, n.y)
                    },
                    "triangle-tee": function (e, t, r) {
                        e.beginPath && e.beginPath();
                        for (var n = t, i = 0; i < n.length; i++) {
                            var a = n[i];
                            e.lineTo(a.x, a.y)
                        }
                        e.closePath && e.closePath(), e.beginPath && e.beginPath();
                        var o = r,
                            s = r[0];
                        e.moveTo(s.x, s.y);
                        for (var i = 0; i < o.length; i++) {
                            var a = o[i];
                            e.lineTo(a.x, a.y)
                        }
                        e.closePath && e.closePath()
                    },
                    circle: function (e, t, r, n) {
                        e.arc(t, r, n, 0, 2 * Math.PI, !1)
                    }
                }))[e]
            }, t.exports = i
        }, {}],
        65: [function (e, t, r) {
            "use strict";
            var n = {};
            n.drawEdge = function (e, t, r, n, i) {
                var a = t._private.rscratch,
                    o = this.usePaths();
                if (!a.badLine && !isNaN(a.allpts[0]) && t.visible()) {
                    var s;
                    r && (s = r, e.translate(-s.x1, -s.y1));
                    var l = t.pstyle("overlay-padding").pfValue,
                        u = t.pstyle("overlay-opacity").value,
                        c = t.pstyle("overlay-color").value;
                    if (i) {
                        if (0 === u) return;
                        this.strokeStyle(e, c[0], c[1], c[2], u), e.lineCap = "round", "self" != a.edgeType || o || (e.lineCap = "butt")
                    } else {
                        var d = t.pstyle("line-color").value;
                        this.strokeStyle(e, d[0], d[1], d[2], t.pstyle("opacity").value), e.lineCap = "butt"
                    }
                    e.lineJoin = "round";
                    var h = t.pstyle("width").pfValue + (i ? 2 * l : 0),
                        p = i ? "solid" : t.pstyle("line-style").value;
                    e.lineWidth = h;
                    var f = t.pstyle("shadow-blur").pfValue,
                        v = t.pstyle("shadow-opacity").value,
                        g = t.pstyle("shadow-color").value,
                        y = t.pstyle("shadow-offset-x").pfValue,
                        m = t.pstyle("shadow-offset-y").pfValue;
                    this.shadowStyle(e, g, i ? 0 : v, f, y, m), this.drawEdgePath(t, e, a.allpts, p, h), this.drawArrowheads(e, t, i), this.shadowStyle(e, "transparent", 0), i || this.drawEdge(e, t, !1, n, !0), this.drawElementText(e, t, n), r && e.translate(s.x1, s.y1)
                }
            }, n.drawEdgePath = function (e, t, r, n, i) {
                var a, o = e._private.rscratch,
                    s = t,
                    l = !1,
                    u = this.usePaths();
                if (u) {
                    var c = r.join("$"),
                        d = o.pathCacheKey && o.pathCacheKey === c;
                    d ? (a = t = o.pathCache, l = !0) : (a = t = new Path2D, o.pathCacheKey = c, o.pathCache = a)
                }
                if (s.setLineDash) switch (n) {
                    case "dotted":
                        s.setLineDash([1, 1]);
                        break;
                    case "dashed":
                        s.setLineDash([6, 3]);
                        break;
                    case "solid":
                        s.setLineDash([])
                }
                if (!l && !o.badLine) switch (t.beginPath && t.beginPath(), t.moveTo(r[0], r[1]), o.edgeType) {
                    case "bezier":
                    case "self":
                    case "compound":
                    case "multibezier":
                        for (var h = 2; h + 3 < r.length; h += 4) t.quadraticCurveTo(r[h], r[h + 1], r[h + 2], r[h + 3]);
                        break;
                    case "straight":
                    case "segments":
                    case "haystack":
                        for (var h = 2; h + 1 < r.length; h += 2) t.lineTo(r[h], r[h + 1])
                }
                t = s, u ? t.stroke(a) : t.stroke(), t.setLineDash && t.setLineDash([])
            }, n.drawArrowheads = function (e, t, r) {
                if (!r) {
                    var n = t._private.rscratch,
                        i = "haystack" === n.edgeType;
                    i || this.drawArrowhead(e, t, "source", n.arrowStartX, n.arrowStartY, n.srcArrowAngle), this.drawArrowhead(e, t, "mid-target", n.midX, n.midY, n.midtgtArrowAngle), this.drawArrowhead(e, t, "mid-source", n.midX, n.midY, n.midsrcArrowAngle), i || this.drawArrowhead(e, t, "target", n.arrowEndX, n.arrowEndY, n.tgtArrowAngle)
                }
            }, n.drawArrowhead = function (e, t, r, n, i, a) {
                if (!(isNaN(n) || null == n || isNaN(i) || null == i || isNaN(a) || null == a)) {
                    var o = this,
                        s = t.pstyle(r + "-arrow-shape").value;
                    if ("none" !== s) {
                        var l = e.globalCompositeOperation,
                            u = "hollow" === t.pstyle(r + "-arrow-fill").value ? "both" : "filled",
                            c = t.pstyle(r + "-arrow-fill").value,
                            d = t.pstyle("opacity").value;
                        "half-triangle-overshot" === s && (c = "hollow", u = "hollow"), 1 === d && "hollow" !== c || (e.globalCompositeOperation = "destination-out", o.fillStyle(e, 255, 255, 255, 1), o.strokeStyle(e, 255, 255, 255, 1), o.drawArrowShape(t, r, e, u, t.pstyle("width").pfValue, t.pstyle(r + "-arrow-shape").value, n, i, a), e.globalCompositeOperation = l);
                        var h = t.pstyle(r + "-arrow-color").value;
                        o.fillStyle(e, h[0], h[1], h[2], d), o.strokeStyle(e, h[0], h[1], h[2], d), o.drawArrowShape(t, r, e, c, t.pstyle("width").pfValue, t.pstyle(r + "-arrow-shape").value, n, i, a)
                    }
                }
            }, n.drawArrowShape = function (e, t, r, n, i, a, o, s, l) {
                var u, c = this,
                    d = this.usePaths(),
                    h = e._private.rscratch,
                    p = !1,
                    f = r,
                    v = {
                        x: o,
                        y: s
                    },
                    g = this.getArrowWidth(i),
                    y = c.arrowShapes[a];
                if (d) {
                    var m = g + "$" + a + "$" + l + "$" + o + "$" + s;
                    h.arrowPathCacheKey = h.arrowPathCacheKey || {}, h.arrowPathCache = h.arrowPathCache || {};
                    var b = h.arrowPathCacheKey[t] === m;
                    b ? (u = r = h.arrowPathCache[t], p = !0) : (u = r = new Path2D, h.arrowPathCacheKey[t] = m, h.arrowPathCache[t] = u)
                }
                r.beginPath && r.beginPath(), p || y.draw(r, g, l, v), !y.leavePathOpen && r.closePath && r.closePath(), r = f, "filled" !== n && "both" !== n || (d ? r.fill(u) : r.fill()), "hollow" !== n && "both" !== n || (r.lineWidth = y.matchEdgeWidth ? i : 1, r.lineJoin = "miter", d ? r.stroke(u) : r.stroke())
            }, t.exports = n
        }, {}],
        66: [function (e, t, r) {
            "use strict";
            var n = e("../../../math"),
                i = {};
            i.drawElement = function (e, t, r, n) {
                var i = this;
                t.isNode() ? i.drawNode(e, t, r, n) : i.drawEdge(e, t, r, n)
            }, i.drawCachedElement = function (e, t, r, i) {
                var a = this,
                    o = t.boundingBox();
                if (0 !== o.w && 0 !== o.h && (!i || n.boundingBoxesIntersect(o, i))) {
                    var s = a.data.eleTxrCache.getElement(t, o, r);
                    s ? e.drawImage(s.texture.canvas, s.x, 0, s.width, s.height, o.x1, o.y1, o.w, o.h) : a.drawElement(e, t)
                }
            }, i.drawElements = function (e, t) {
                for (var r = this, n = 0; n < t.length; n++) {
                    var i = t[n];
                    r.drawElement(e, i)
                }
            }, i.drawCachedElements = function (e, t, r, n) {
                for (var i = this, a = 0; a < t.length; a++) {
                    var o = t[a];
                    i.drawCachedElement(e, o, r, n)
                }
            }, i.drawCachedNodes = function (e, t, r, n) {
                for (var i = this, a = 0; a < t.length; a++) {
                    var o = t[a];
                    o.isNode() && i.drawCachedElement(e, o, r, n)
                }
            }, i.drawLayeredElements = function (e, t, r, n) {
                var i = this,
                    a = i.data.lyrTxrCache.getLayers(t, r);
                if (a)
                    for (var o = 0; o < a.length; o++) {
                        var s = a[o],
                            l = s.bb;
                        0 !== l.w && 0 !== l.h && e.drawImage(s.canvas, l.x1, l.y1, l.w, l.h)
                    } else i.drawCachedElements(e, t, r, n)
            }, t.exports = i
        }, {
            "../../../math": 85
        }],
        67: [function (e, t, r) {
            "use strict";
            var n = {};
            n.safeDrawImage = function (e, t, r, n, i, a, o, s, l, u) {
                var c = this;
                try {
                    e.drawImage(t, r, n, i, a, o, s, l, u)
                } catch (d) {
                    c.redrawHint("eles", !0), c.redrawHint("drag", !0), c.drawingImage = !0, c.redraw()
                }
            }, n.drawInscribedImage = function (e, t, r) {
                var n = this,
                    i = r._private.position.x,
                    a = r._private.position.y,
                    o = r.pstyle("background-fit").value,
                    s = r.pstyle("background-position-x"),
                    l = r.pstyle("background-position-y"),
                    u = r.pstyle("background-repeat").value,
                    c = r.width(),
                    d = r.height(),
                    h = r._private.rscratch,
                    p = r.pstyle("background-clip").value,
                    f = "node" === p,
                    v = r.pstyle("background-image-opacity").value,
                    g = t.width || t.cachedW,
                    y = t.height || t.cachedH;
                null != g && null != y || (document.body.appendChild(t), g = t.cachedW = t.width || t.offsetWidth, y = t.cachedH = t.height || t.offsetHeight, document.body.removeChild(t));
                var m = g,
                    b = y,
                    x = r.pstyle("background-width");
                "auto" !== x.value && (m = "%" === x.units ? x.value / 100 * c : x.pfValue);
                var w = r.pstyle("background-height");
                if ("auto" !== w.value && (b = "%" === w.units ? w.value / 100 * d : w.pfValue), 0 !== m && 0 !== b) {
                    if ("contain" === o) {
                        var E = Math.min(c / m, d / b);
                        m *= E, b *= E
                    } else if ("cover" === o) {
                        var E = Math.max(c / m, d / b);
                        m *= E, b *= E
                    }
                    var _ = i - c / 2;
                    _ += "%" === s.units ? (c - m) * s.value / 100 : s.pfValue;
                    var S = a - d / 2;
                    S += "%" === l.units ? (d - b) * l.value / 100 : l.pfValue, h.pathCache && (_ -= i, S -= a, i = 0, a = 0);
                    var P = e.globalAlpha;
                    if (e.globalAlpha = v, "no-repeat" === u) f && (e.save(), h.pathCache ? e.clip(h.pathCache) : (n.nodeShapes[n.getNodeShape(r)].draw(e, i, a, c, d), e.clip())), n.safeDrawImage(e, t, 0, 0, g, y, _, S, m, b), f && e.restore();
                    else {
                        var T = e.createPattern(t, u);
                        e.fillStyle = T, n.nodeShapes[n.getNodeShape(r)].draw(e, i, a, c, d), e.translate(_, S), e.fill(), e.translate(-_, -S)
                    }
                    e.globalAlpha = P
                }
            }, t.exports = n
        }, {}],
        68: [function (e, t, r) {
            "use strict";

            function n(e, t, r, n, i, a) {
                var a = a || 5;
                e.beginPath(), e.moveTo(t + a, r), e.lineTo(t + n - a, r), e.quadraticCurveTo(t + n, r, t + n, r + a), e.lineTo(t + n, r + i - a), e.quadraticCurveTo(t + n, r + i, t + n - a, r + i), e.lineTo(t + a, r + i), e.quadraticCurveTo(t, r + i, t, r + i - a), e.lineTo(t, r + a), e.quadraticCurveTo(t, r, t + a, r), e.closePath(), e.fill()
            }
            var i = e("../../../util"),
                a = e("../../../math"),
                o = {};
            o.eleTextBiggerThanMin = function (e, t) {
                if (!t) {
                    var r = e.cy().zoom(),
                        n = this.getPixelRatio(),
                        i = Math.ceil(a.log2(r * n));
                    t = Math.pow(2, i)
                }
                var o = e.pstyle("font-size").pfValue * t,
                    s = e.pstyle("min-zoomed-font-size").pfValue;
                return !(s > o)
            }, o.drawElementText = function (e, t, r) {
                var n = this;
                if (void 0 === r) {
                    if (!n.eleTextBiggerThanMin(t)) return
                } else if (!r) return;
                if (t.isNode()) {
                    var i = t.pstyle("label");
                    if (!i || !i.value) return;
                    var a = t.pstyle("text-halign").strValue;
                    t.pstyle("text-valign").strValue;
                    switch (a) {
                        case "left":
                            e.textAlign = "right";
                            break;
                        case "right":
                            e.textAlign = "left";
                            break;
                        default:
                            e.textAlign = "center"
                    }
                    e.textBaseline = "bottom"
                } else {
                    var i = t.pstyle("label"),
                        o = t.pstyle("source-label"),
                        s = t.pstyle("target-label");
                    if (!(i && i.value || o && o.value || s && s.value)) return;
                    e.textAlign = "center", e.textBaseline = "bottom"
                }
                n.drawText(e, t), t.isEdge() && (n.drawText(e, t, "source"), n.drawText(e, t, "target"))
            }, o.drawNodeText = o.drawEdgeText = o.drawElementText, o.getFontCache = function (e) {
                var t;
                this.fontCaches = this.fontCaches || [];
                for (var r = 0; r < this.fontCaches.length; r++)
                    if (t = this.fontCaches[r], t.context === e) return t;
                return t = {
                    context: e
                }, this.fontCaches.push(t), t
            }, o.setupTextStyle = function (e, t) {
                var r = t.effectiveOpacity(),
                    n = t.pstyle("font-style").strValue,
                    i = t.pstyle("font-size").pfValue + "px",
                    a = t.pstyle("font-family").strValue,
                    o = t.pstyle("font-weight").strValue,
                    s = t.pstyle("text-opacity").value * t.pstyle("opacity").value * r,
                    l = t.pstyle("text-outline-opacity").value * s,
                    u = t.pstyle("color").value,
                    c = t.pstyle("text-outline-color").value,
                    d = t.pstyle("text-shadow-blur").pfValue,
                    h = t.pstyle("text-shadow-opacity").value,
                    p = t.pstyle("text-shadow-color").value,
                    f = t.pstyle("text-shadow-offset-x").pfValue,
                    v = t.pstyle("text-shadow-offset-y").pfValue,
                    g = t._private.fontKey,
                    y = this.getFontCache(e);
                y.key !== g && (e.font = n + " " + o + " " + i + " " + a, y.key = g), e.lineJoin = "round", this.fillStyle(e, u[0], u[1], u[2], s), this.strokeStyle(e, c[0], c[1], c[2], l), this.shadowStyle(e, p, h, d, f, v)
            }, o.drawText = function (e, t, r) {
                var a = t._private,
                    o = a.rscratch,
                    s = t.effectiveOpacity();
                if (0 !== s && 0 !== t.pstyle("text-opacity").value) {
                    var l = i.getPrefixedProperty(o, "labelX", r),
                        u = i.getPrefixedProperty(o, "labelY", r),
                        c = this.getLabelText(t, r);
                    if (null != c && "" !== c && !isNaN(l) && !isNaN(u)) {
                        this.setupTextStyle(e, t);
                        var d = r ? r + "-" : "",
                            h = i.getPrefixedProperty(o, "labelWidth", r),
                            p = i.getPrefixedProperty(o, "labelHeight", r),
                            f = i.getPrefixedProperty(o, "labelAngle", r),
                            v = t.pstyle(d + "text-margin-x").pfValue,
                            g = t.pstyle(d + "text-margin-y").pfValue,
                            y = t.isEdge(),
                            m = (t.isNode(), t.pstyle("text-halign").value),
                            b = t.pstyle("text-valign").value;
                        y && (m = "center", b = "center"), l += v, u += g;
                        var x, w = t.pstyle("text-rotation");
                        if (x = "autorotate" === w.strValue ? y ? f : 0 : "none" === w.strValue ? 0 : w.pfValue, 0 !== x) {
                            var E = l,
                                _ = u;
                            e.translate(E, _), e.rotate(x), l = 0, u = 0
                        }
                        switch (b) {
                            case "top":
                                break;
                            case "center":
                                u += p / 2;
                                break;
                            case "bottom":
                                u += p
                        }
                        var S = t.pstyle("text-background-opacity").value,
                            P = t.pstyle("text-border-opacity").value,
                            T = t.pstyle("text-border-width").pfValue;
                        if (S > 0 || T > 0 && P > 0) {
                            var k = l;
                            switch (m) {
                                case "left":
                                    k -= h;
                                    break;
                                case "center":
                                    k -= h / 2;
                                    break;
                                case "right":
                            }
                            var D = u - p;
                            if (S > 0) {
                                var C = e.fillStyle,
                                    M = t.pstyle("text-background-color").value;
                                e.fillStyle = "rgba(" + M[0] + "," + M[1] + "," + M[2] + "," + S * s + ")";
                                var N = t.pstyle("text-background-shape").strValue;
                                "roundrectangle" == N ? n(e, k, D, h, p, 2) : e.fillRect(k, D, h, p), e.fillStyle = C
                            }
                            if (T > 0 && P > 0) {
                                var B = e.strokeStyle,
                                    z = e.lineWidth,
                                    I = t.pstyle("text-border-color").value,
                                    L = t.pstyle("text-border-style").value;
                                if (e.strokeStyle = "rgba(" + I[0] + "," + I[1] + "," + I[2] + "," + P * s + ")", e.lineWidth = T, e.setLineDash) switch (L) {
                                    case "dotted":
                                        e.setLineDash([1, 1]);
                                        break;
                                    case "dashed":
                                        e.setLineDash([4, 2]);
                                        break;
                                    case "double":
                                        e.lineWidth = T / 4, e.setLineDash([]);
                                        break;
                                    case "solid":
                                        e.setLineDash([])
                                }
                                if (e.strokeRect(k, D, h, p), "double" === L) {
                                    var O = T / 2;
                                    e.strokeRect(k + O, D + O, h - 2 * O, p - 2 * O)
                                }
                                e.setLineDash && e.setLineDash([]), e.lineWidth = z, e.strokeStyle = B
                            }
                        }
                        var A = 2 * t.pstyle("text-outline-width").pfValue;
                        if (A > 0 && (e.lineWidth = A), "wrap" === t.pstyle("text-wrap").value) {
                            var R = i.getPrefixedProperty(o, "labelWrapCachedLines", r),
                                q = p / R.length;
                            switch (b) {
                                case "top":
                                    u -= (R.length - 1) * q;
                                    break;
                                case "center":
                                case "bottom":
                                    u -= (R.length - 1) * q
                            }
                            for (var V = 0; V < R.length; V++) A > 0 && e.strokeText(R[V], l, u), e.fillText(R[V], l, u), u += q
                        } else A > 0 && e.strokeText(c, l, u), e.fillText(c, l, u);
                        0 !== x && (e.rotate(-x), e.translate(-E, -_)), this.shadowStyle(e, "transparent", 0)
                    }
                }
            }, t.exports = o
        }, {
            "../../../math": 85,
            "../../../util": 100
        }],
        69: [function (e, t, r) {
            "use strict";
            var n = e("../../../is"),
                i = {};
            i.drawNode = function (e, t, r, i) {
                var a, o, s = this,
                    l = t._private.rscratch,
                    u = t._private,
                    c = c || u.position;
                if (n.number(c.x) && n.number(c.y) && t.visible()) {
                    var d, h = t.effectiveOpacity(),
                        p = this.usePaths(),
                        f = !1,
                        v = t.pstyle("padding").pfValue;
                    a = t.width() + 2 * v, o = t.height() + 2 * v, e.lineWidth = t.pstyle("border-width").pfValue;
                    var g;
                    r && (g = r, e.translate(-g.x1, -g.y1));
                    var y, m = t.pstyle("background-image"),
                        b = m.value[2] || m.value[1];
                    if (void 0 !== b) {
                        var x = t.pstyle("background-image-crossorigin");
                        y = this.getCachedImage(b, x, function () {
                            t.trigger("background"), s.redrawHint("eles", !0), s.redrawHint("drag", !0), s.drawingImage = !0, s.redraw()
                        });
                        var w = u.backgrounding;
                        u.backgrounding = !y.complete, w !== u.backgrounding && t.updateStyle(!1)
                    }
                    var E = t.pstyle("background-color").value,
                        _ = t.pstyle("border-color").value,
                        S = t.pstyle("border-style").value;
                    this.fillStyle(e, E[0], E[1], E[2], t.pstyle("background-opacity").value * h), this.strokeStyle(e, _[0], _[1], _[2], t.pstyle("border-opacity").value * h);
                    var P = t.pstyle("shadow-blur").pfValue,
                        T = t.pstyle("shadow-opacity").value,
                        k = t.pstyle("shadow-color").value,
                        D = t.pstyle("shadow-offset-x").pfValue,
                        C = t.pstyle("shadow-offset-y").pfValue;
                    if (this.shadowStyle(e, k, T, P, D, C), e.lineJoin = "miter", e.setLineDash) switch (S) {
                        case "dotted":
                            e.setLineDash([1, 1]);
                            break;
                        case "dashed":
                            e.setLineDash([4, 2]);
                            break;
                        case "solid":
                        case "double":
                            e.setLineDash([])
                    }
                    var M = t.pstyle("shape").strValue,
                        N = t.pstyle("shape-polygon-points").pfValue;
                    if (p) {
                        var B = M + "$" + a + "$" + o + ("polygon" === M ? "$" + N.join("$") : "");
                        e.translate(c.x, c.y), l.pathCacheKey === B ? (d = l.pathCache, f = !0) : (d = new Path2D, l.pathCacheKey = B, l.pathCache = d)
                    }
                    if (!f) {
                        var z = c;
                        p && (z = {
                            x: 0,
                            y: 0
                        }), s.nodeShapes[this.getNodeShape(t)].draw(d || e, z.x, z.y, a, o)
                    }
                    p ? e.fill(d) : e.fill(), this.shadowStyle(e, "transparent", 0), void 0 !== b && y.complete && this.drawInscribedImage(e, y, t);
                    var I = t.pstyle("background-blacken").value,
                        L = t.pstyle("border-width").pfValue;
                    if (this.hasPie(t) && (this.drawPie(e, t, h), 0 === I && 0 === L || p || s.nodeShapes[this.getNodeShape(t)].draw(e, c.x, c.y, a, o)), I > 0 ? (this.fillStyle(e, 0, 0, 0, I), p ? e.fill(d) : e.fill()) : 0 > I && (this.fillStyle(e, 255, 255, 255, -I), p ? e.fill(d) : e.fill()), L > 0 && (p ? e.stroke(d) : e.stroke(), "double" === S)) {
                        e.lineWidth = t.pstyle("border-width").pfValue / 3;
                        var O = e.globalCompositeOperation;
                        e.globalCompositeOperation = "destination-out", p ? e.stroke(d) : e.stroke(), e.globalCompositeOperation = O
                    }
                    p && e.translate(-c.x, -c.y), e.setLineDash && e.setLineDash([]), s.drawElementText(e, t, i);
                    var A = t.pstyle("overlay-padding").pfValue,
                        R = t.pstyle("overlay-opacity").value,
                        q = t.pstyle("overlay-color").value;
                    R > 0 && (this.fillStyle(e, q[0], q[1], q[2], R), s.nodeShapes.roundrectangle.draw(e, t._private.position.x, t._private.position.y, a + 2 * A, o + 2 * A), e.fill()), r && e.translate(g.x1, g.y1)
                }
            }, i.hasPie = function (e) {
                return e = e[0], e._private.hasPie
            }, i.drawPie = function (e, t, r, n) {
                t = t[0];
                var i = t._private,
                    a = t.cy().style(),
                    o = t.pstyle("pie-size"),
                    s = t.width(),
                    l = t.height(),
                    n = n || i.position,
                    u = n.x,
                    c = n.y,
                    d = Math.min(s, l) / 2,
                    h = 0,
                    p = this.usePaths();
                p && (u = 0, c = 0), "%" === o.units ? d = d * o.value / 100 : void 0 !== o.pfValue && (d = o.pfValue / 2);
                for (var f = 1; f <= a.pieBackgroundN; f++) {
                    var v = t.pstyle("pie-" + f + "-background-size").value,
                        g = t.pstyle("pie-" + f + "-background-color").value,
                        y = t.pstyle("pie-" + f + "-background-opacity").value * r,
                        m = v / 100;
                    m + h > 1 && (m = 1 - h);
                    var b = 1.5 * Math.PI + 2 * Math.PI * h,
                        x = 2 * Math.PI * m,
                        w = b + x;
                    0 === v || h >= 1 || h + m > 1 || (e.beginPath(), e.moveTo(u, c), e.arc(u, c, d, b, w), e.closePath(), this.fillStyle(e, g[0], g[1], g[2], y), e.fill(), h += m)
                }
            }, t.exports = i
        }, {
            "../../../is": 83
        }],
        70: [function (e, t, r) {
            "use strict";
            var n = {},
                i = e("../../../util"),
                a = 100;
            n.getPixelRatio = function () {
                var e = this.data.contexts[0];
                if (null != this.forcedPixelRatio) return this.forcedPixelRatio;
                var t = e.backingStorePixelRatio || e.webkitBackingStorePixelRatio || e.mozBackingStorePixelRatio || e.msBackingStorePixelRatio || e.oBackingStorePixelRatio || e.backingStorePixelRatio || 1;
                return (window.devicePixelRatio || 1) / t
            }, n.paintCache = function (e) {
                for (var t, r = this.paintCaches = this.paintCaches || [], n = !0, i = 0; i < r.length; i++)
                    if (t = r[i], t.context === e) {
                        n = !1;
                        break
                    }
                return n && (t = {
                    context: e
                }, r.push(t)), t
            }, n.fillStyle = function (e, t, r, n, i) {
                e.fillStyle = "rgba(" + t + "," + r + "," + n + "," + i + ")"
            }, n.strokeStyle = function (e, t, r, n, i) {
                e.strokeStyle = "rgba(" + t + "," + r + "," + n + "," + i + ")"
            }, n.shadowStyle = function (e, t, r, n, i, a) {
                var o = this.cy.zoom();
                r > 0 ? (e.shadowBlur = n * o, e.shadowColor = "rgba(" + t[0] + "," + t[1] + "," + t[2] + "," + r + ")", e.shadowOffsetX = i * o, e.shadowOffsetY = a * o) : (e.shadowBlur = 0, e.shadowColor = "transparent", e.shadowOffsetX = 0, e.shadowOffsetY = 0)
            }, n.matchCanvasSize = function (e) {
                var t = this,
                    r = t.data,
                    n = t.findContainerClientCoords(),
                    i = n[2],
                    a = n[3],
                    o = t.getPixelRatio(),
                    s = t.motionBlurPxRatio;
                e !== t.data.bufferCanvases[t.MOTIONBLUR_BUFFER_NODE] && e !== t.data.bufferCanvases[t.MOTIONBLUR_BUFFER_DRAG] || (o = s);
                var l, u = i * o,
                    c = a * o;
                if (u !== t.canvasWidth || c !== t.canvasHeight) {
                    t.fontCaches = null;
                    var d = r.canvasContainer;
                    d.style.width = i + "px", d.style.height = a + "px";
                    for (var h = 0; h < t.CANVAS_LAYERS; h++) l = r.canvases[h], l.width = u, l.height = c, l.style.width = i + "px", l.style.height = a + "px";
                    for (var h = 0; h < t.BUFFER_COUNT; h++) l = r.bufferCanvases[h], l.width = u, l.height = c, l.style.width = i + "px", l.style.height = a + "px";
                    t.textureMult = 1, 1 >= o && (l = r.bufferCanvases[t.TEXTURE_BUFFER], t.textureMult = 2, l.width = u * t.textureMult, l.height = c * t.textureMult), t.canvasWidth = u, t.canvasHeight = c
                }
            }, n.renderTo = function (e, t, r, n) {
                this.render({
                    forcedContext: e,
                    forcedZoom: t,
                    forcedPan: r,
                    drawAllLayers: !0,
                    forcedPxRatio: n
                })
            }, n.render = function (e) {
                function t(e, t, r, n, i) {
                    var a = e.globalCompositeOperation;
                    e.globalCompositeOperation = "destination-out", c.fillStyle(e, 255, 255, 255, c.motionBlurTransparency), e.fillRect(t, r, n, i), e.globalCompositeOperation = a
                }

                function r(e, r) {
                    var i, a, s, d;
                    c.clearingMotionBlur || e !== p.bufferContexts[c.MOTIONBLUR_BUFFER_NODE] && e !== p.bufferContexts[c.MOTIONBLUR_BUFFER_DRAG] ? (i = T, a = S, s = c.canvasWidth, d = c.canvasHeight) : (i = {
                        x: P.x * y,
                        y: P.y * y
                    }, a = _ * y, s = c.canvasWidth * y, d = c.canvasHeight * y), e.setTransform(1, 0, 0, 1, 0, 0), "motionBlur" === r ? t(e, 0, 0, s, d) : n || void 0 !== r && !r || e.clearRect(0, 0, s, d), o || (e.translate(i.x, i.y), e.scale(a, a)), u && e.translate(u.x, u.y), l && e.scale(l, l)
                }
                e = e || i.staticEmptyObject();
                var n = e.forcedContext,
                    o = e.drawAllLayers,
                    s = e.drawOnlyNodeLayer,
                    l = e.forcedZoom,
                    u = e.forcedPan,
                    c = this,
                    d = void 0 === e.forcedPxRatio ? this.getPixelRatio() : e.forcedPxRatio,
                    h = c.cy,
                    p = c.data,
                    f = p.canvasNeedsRedraw,
                    v = c.textureOnViewport && !n && (c.pinching || c.hoverData.dragging || c.swipePanning || c.data.wheelZooming),
                    g = void 0 !== e.motionBlur ? e.motionBlur : c.motionBlur,
                    y = c.motionBlurPxRatio,
                    m = h.hasCompoundNodes(),
                    b = c.hoverData.draggingEles,
                    x = !(!c.hoverData.selecting && !c.touchData.selecting);
                g = g && !n && c.motionBlurEnabled && !x;
                var w = g;
                n || (c.prevPxRatio !== d && (c.invalidateContainerClientCoordsCache(), c.matchCanvasSize(c.container), c.redrawHint("eles", !0), c.redrawHint("drag", !0)), c.prevPxRatio = d), !n && c.motionBlurTimeout && clearTimeout(c.motionBlurTimeout), g && (null == c.mbFrames && (c.mbFrames = 0), c.drawingImage || c.mbFrames++ , c.mbFrames < 3 && (w = !1), c.mbFrames > c.minMbLowQualFrames && (c.motionBlurPxRatio = c.mbPxRBlurry)), c.clearingMotionBlur && (c.motionBlurPxRatio = 1), c.textureDrawLastFrame && !v && (f[c.NODE] = !0, f[c.SELECT_BOX] = !0);
                var E = h.style()._private.coreStyle,
                    _ = h.zoom(),
                    S = void 0 !== l ? l : _,
                    P = h.pan(),
                    T = {
                        x: P.x,
                        y: P.y
                    },
                    k = {
                        zoom: _,
                        pan: {
                            x: P.x,
                            y: P.y
                        }
                    },
                    D = c.prevViewport,
                    C = void 0 === D || k.zoom !== D.zoom || k.pan.x !== D.pan.x || k.pan.y !== D.pan.y;
                C || b && !m || (c.motionBlurPxRatio = 1), u && (T = u), S *= d, T.x *= d, T.y *= d;
                var M = c.getCachedZSortedEles();
                if (v || (c.textureDrawLastFrame = !1), v) {
                    c.textureDrawLastFrame = !0;
                    var N;
                    if (!c.textureCache) {
                        c.textureCache = {}, N = c.textureCache.bb = h.mutableElements().boundingBox(), c.textureCache.texture = c.data.bufferCanvases[c.TEXTURE_BUFFER];
                        var B = c.data.bufferContexts[c.TEXTURE_BUFFER];
                        B.setTransform(1, 0, 0, 1, 0, 0), B.clearRect(0, 0, c.canvasWidth * c.textureMult, c.canvasHeight * c.textureMult), c.render({
                            forcedContext: B,
                            drawOnlyNodeLayer: !0,
                            forcedPxRatio: d * c.textureMult
                        });
                        var k = c.textureCache.viewport = {
                            zoom: h.zoom(),
                            pan: h.pan(),
                            width: c.canvasWidth,
                            height: c.canvasHeight
                        };
                        k.mpan = {
                            x: (0 - k.pan.x) / k.zoom,
                            y: (0 - k.pan.y) / k.zoom
                        }
                    }
                    f[c.DRAG] = !1, f[c.NODE] = !1;
                    var z = p.contexts[c.NODE],
                        I = c.textureCache.texture,
                        k = c.textureCache.viewport;
                    N = c.textureCache.bb, z.setTransform(1, 0, 0, 1, 0, 0), g ? t(z, 0, 0, k.width, k.height) : z.clearRect(0, 0, k.width, k.height);
                    var L = E["outside-texture-bg-color"].value,
                        O = E["outside-texture-bg-opacity"].value;
                    c.fillStyle(z, L[0], L[1], L[2], O), z.fillRect(0, 0, k.width, k.height);
                    var _ = h.zoom();
                    r(z, !1), z.clearRect(k.mpan.x, k.mpan.y, k.width / k.zoom / d, k.height / k.zoom / d), z.drawImage(I, k.mpan.x, k.mpan.y, k.width / k.zoom / d, k.height / k.zoom / d)
                } else c.textureOnViewport && !n && (c.textureCache = null);
                var A = h.extent(),
                    R = c.pinching || c.hoverData.dragging || c.swipePanning || c.data.wheelZooming || c.hoverData.draggingEles,
                    q = c.hideEdgesOnViewport && R,
                    V = [];
                if (V[c.NODE] = !f[c.NODE] && g && !c.clearedForMotionBlur[c.NODE] || c.clearingMotionBlur, V[c.NODE] && (c.clearedForMotionBlur[c.NODE] = !0), V[c.DRAG] = !f[c.DRAG] && g && !c.clearedForMotionBlur[c.DRAG] || c.clearingMotionBlur, V[c.DRAG] && (c.clearedForMotionBlur[c.DRAG] = !0), f[c.NODE] || o || s || V[c.NODE]) {
                    var F = g && !V[c.NODE] && 1 !== y,
                        z = n || (F ? c.data.bufferContexts[c.MOTIONBLUR_BUFFER_NODE] : p.contexts[c.NODE]),
                        j = g && !F ? "motionBlur" : void 0;
                    r(z, j), q ? c.drawCachedNodes(z, M.nondrag, d, A) : c.drawLayeredElements(z, M.nondrag, d, A), o || g || (f[c.NODE] = !1)
                }
                if (!s && (f[c.DRAG] || o || V[c.DRAG])) {
                    var F = g && !V[c.DRAG] && 1 !== y,
                        z = n || (F ? c.data.bufferContexts[c.MOTIONBLUR_BUFFER_DRAG] : p.contexts[c.DRAG]);
                    r(z, g && !F ? "motionBlur" : void 0), q ? c.drawCachedNodes(z, M.drag, d, A) : c.drawCachedElements(z, M.drag, d, A), o || g || (f[c.DRAG] = !1)
                }
                if (c.showFps || !s && f[c.SELECT_BOX] && !o) {
                    var z = n || p.contexts[c.SELECT_BOX];
                    if (r(z), 1 == c.selection[4] && (c.hoverData.selecting || c.touchData.selecting)) {
                        var _ = c.cy.zoom(),
                            X = E["selection-box-border-width"].value / _;
                        z.lineWidth = X, z.fillStyle = "rgba(" + E["selection-box-color"].value[0] + "," + E["selection-box-color"].value[1] + "," + E["selection-box-color"].value[2] + "," + E["selection-box-opacity"].value + ")", z.fillRect(c.selection[0], c.selection[1], c.selection[2] - c.selection[0], c.selection[3] - c.selection[1]), X > 0 && (z.strokeStyle = "rgba(" + E["selection-box-border-color"].value[0] + "," + E["selection-box-border-color"].value[1] + "," + E["selection-box-border-color"].value[2] + "," + E["selection-box-opacity"].value + ")", z.strokeRect(c.selection[0], c.selection[1], c.selection[2] - c.selection[0], c.selection[3] - c.selection[1]))
                    }
                    if (p.bgActivePosistion && !c.hoverData.selecting) {
                        var _ = c.cy.zoom(),
                            Y = p.bgActivePosistion;
                        z.fillStyle = "rgba(" + E["active-bg-color"].value[0] + "," + E["active-bg-color"].value[1] + "," + E["active-bg-color"].value[2] + "," + E["active-bg-opacity"].value + ")", z.beginPath(), z.arc(Y.x, Y.y, E["active-bg-size"].pfValue / _, 0, 2 * Math.PI), z.fill()
                    }
                    var W = c.lastRedrawTime;
                    if (c.showFps && W) {
                        W = Math.round(W);
                        var $ = Math.round(1e3 / W);
                        z.setTransform(1, 0, 0, 1, 0, 0), z.fillStyle = "rgba(255, 0, 0, 0.75)", z.strokeStyle = "rgba(255, 0, 0, 0.75)", z.lineWidth = 1, z.fillText("1 frame = " + W + " ms = " + $ + " fps", 0, 20);
                        var H = 60;
                        z.strokeRect(0, 30, 250, 20), z.fillRect(0, 30, 250 * Math.min($ / H, 1), 20)
                    }
                    o || (f[c.SELECT_BOX] = !1)
                }
                if (g && 1 !== y) {
                    var U = p.contexts[c.NODE],
                        Z = c.data.bufferCanvases[c.MOTIONBLUR_BUFFER_NODE],
                        G = p.contexts[c.DRAG],
                        Q = c.data.bufferCanvases[c.MOTIONBLUR_BUFFER_DRAG],
                        K = function (e, r, n) {
                            e.setTransform(1, 0, 0, 1, 0, 0), n || !w ? e.clearRect(0, 0, c.canvasWidth, c.canvasHeight) : t(e, 0, 0, c.canvasWidth, c.canvasHeight);
                            var i = y;
                            e.drawImage(r, 0, 0, c.canvasWidth * i, c.canvasHeight * i, 0, 0, c.canvasWidth, c.canvasHeight)
                        };
                    (f[c.NODE] || V[c.NODE]) && (K(U, Z, V[c.NODE]), f[c.NODE] = !1), (f[c.DRAG] || V[c.DRAG]) && (K(G, Q, V[c.DRAG]), f[c.DRAG] = !1)
                }
                c.prevViewport = k, c.clearingMotionBlur && (c.clearingMotionBlur = !1, c.motionBlurCleared = !0, c.motionBlur = !0), g && (c.motionBlurTimeout = setTimeout(function () {
                    c.motionBlurTimeout = null, c.clearedForMotionBlur[c.NODE] = !1, c.clearedForMotionBlur[c.DRAG] = !1, c.motionBlur = !1, c.clearingMotionBlur = !v, c.mbFrames = 0, f[c.NODE] = !0, f[c.DRAG] = !0, c.redraw()
                }, a)), c.drawingImage = !1, n || c.initrender || (c.initrender = !0, h.trigger("initrender")), n || h.trigger("render")
            }, t.exports = n
        }, {
            "../../../util": 100
        }],
        71: [function (e, t, r) {
            "use strict";
            var n = e("../../../math"),
                i = {};
            i.drawPolygonPath = function (e, t, r, n, i, a) {
                var o = n / 2,
                    s = i / 2;
                e.beginPath && e.beginPath(), e.moveTo(t + o * a[0], r + s * a[1]);
                for (var l = 1; l < a.length / 2; l++) e.lineTo(t + o * a[2 * l], r + s * a[2 * l + 1]);
                e.closePath()
            }, i.drawRoundRectanglePath = function (e, t, r, i, a) {
                var o = i / 2,
                    s = a / 2,
                    l = n.getRoundRectangleRadius(i, a);
                e.beginPath && e.beginPath(), e.moveTo(t, r - s), e.arcTo(t + o, r - s, t + o, r, l), e.arcTo(t + o, r + s, t, r + s, l), e.arcTo(t - o, r + s, t - o, r, l), e.arcTo(t - o, r - s, t, r - s, l), e.lineTo(t, r - s), e.closePath()
            };
            for (var a = Math.sin(0), o = Math.cos(0), s = {}, l = {}, u = Math.PI / 40, c = 0 * Math.PI; c < 2 * Math.PI; c += u) s[c] = Math.sin(c), l[c] = Math.cos(c);
            i.drawEllipsePath = function (e, t, r, n, i) {
                if (e.beginPath && e.beginPath(), e.ellipse) e.ellipse(t, r, n / 2, i / 2, 0, 0, 2 * Math.PI);
                else
                    for (var c, d, h = n / 2, p = i / 2, f = 0 * Math.PI; f < 2 * Math.PI; f += u) c = t - h * s[f] * a + h * l[f] * o, d = r + p * l[f] * a + p * s[f] * o, 0 === f ? e.moveTo(c, d) : e.lineTo(c, d);
                e.closePath()
            }, t.exports = i
        }, {
            "../../../math": 85
        }],
        72: [function (e, t, r) {
            "use strict";
            var n = e("../../../math"),
                i = e("../../../util"),
                a = e("../../../heap"),
                o = e("./texture-cache-defs"),
                s = 25,
                l = 50,
                u = -4,
                c = 2,
                d = 3.99,
                h = 8,
                p = 1024,
                f = 1024,
                v = 1024,
                g = .5,
                y = .8,
                m = 10,
                b = !1,
                x = !1,
                w = .15,
                E = .1,
                _ = .9,
                S = .9,
                P = 100,
                T = 1,
                k = {
                    dequeue: "dequeue",
                    downscale: "downscale",
                    highQuality: "highQuality"
                },
                D = function (e) {
                    var t = this;
                    t.renderer = e, t.onDequeues = [], t.setupDequeueing()
                },
                C = D.prototype;
            C.reasons = k, C.getTextureQueue = function (e) {
                var t = this;
                return t.eleImgCaches = t.eleImgCaches || {}, t.eleImgCaches[e] = t.eleImgCaches[e] || []
            }, C.getRetiredTextureQueue = function (e) {
                var t = this,
                    r = t.eleImgCaches.retired = t.eleImgCaches.retired || {},
                    n = r[e] = r[e] || [];
                return n
            }, C.getElementQueue = function () {
                var e = this,
                    t = e.eleCacheQueue = e.eleCacheQueue || new a(function (e, t) {
                        return t.reqs - e.reqs
                    });
                return t
            }, C.getElementIdToQueue = function () {
                var e = this,
                    t = e.eleIdToCacheQueue = e.eleIdToCacheQueue || {};
                return t
            }, C.getElement = function (e, t, r, i, a) {
                var o = this,
                    p = this.renderer,
                    g = e._private.rscratch,
                    y = p.cy.zoom();
                if (0 === t.w || 0 === t.h) return null;
                if (null == i && (i = Math.ceil(n.log2(y * r))), u > i) i = u;
                else if (y >= d || i > c) return null;
                var m = Math.pow(2, i),
                    w = t.h * m,
                    E = t.w * m,
                    _ = g.imgCaches = g.imgCaches || {},
                    S = _[i];
                if (S) return S;
                var P;
                if (P = s >= w ? s : l >= w ? l : Math.ceil(w / l) * l, w > v || E > f || !b && e.isEdge() || !x && e.isParent()) return null;
                var T = o.getTextureQueue(P),
                    D = T[T.length - 2],
                    C = function () {
                        return o.recycleTexture(P, E) || o.addTexture(P, E)
                    };
                D || (D = T[T.length - 1]), D || (D = C()), D.width - D.usedWidth < E && (D = C());
                for (var M, N = p.eleTextBiggerThanMin(e, m), B = function (e) {
                    return e && e.scaledLabelShown === N
                }, z = a && a === k.dequeue, I = a && a === k.highQuality, L = a && a === k.downscale, O = i + 1; c >= O; O++) {
                    var A = _[O];
                    if (A) {
                        M = A;
                        break
                    }
                }
                var R = M && M.level === i + 1 ? M : null,
                    q = function () {
                        D.context.drawImage(R.texture.canvas, R.x, 0, R.width, R.height, D.usedWidth, 0, E, w)
                    };
                if (B(R)) q();
                else if (B(M)) {
                    if (!I) return o.queueElement(e, t, M.level - 1), M;
                    for (var O = M.level; O > i; O--) R = o.getElement(e, t, r, O, k.downscale);
                    q()
                } else {
                    var V;
                    if (!z && !I && !L)
                        for (var O = i - 1; O >= u; O--) {
                            var A = _[O];
                            if (A) {
                                V = A;
                                break
                            }
                        }
                    if (B(V)) return o.queueElement(e, t, i), V;
                    D.context.translate(D.usedWidth, 0), D.context.scale(m, m), p.drawElement(D.context, e, t, N), D.context.scale(1 / m, 1 / m), D.context.translate(-D.usedWidth, 0)
                }
                return S = _[i] = {
                    ele: e,
                    x: D.usedWidth,
                    texture: D,
                    level: i,
                    scale: m,
                    width: E,
                    height: w,
                    scaledLabelShown: N
                }, D.usedWidth += Math.ceil(E + h), D.eleCaches.push(S), o.checkTextureFullness(D), S
            }, C.invalidateElement = function (e) {
                var t = this,
                    r = e._private.rscratch.imgCaches;
                if (r)
                    for (var n = u; c >= n; n++) {
                        var a = r[n];
                        if (a) {
                            var o = a.texture;
                            o.invalidatedWidth += a.width, r[n] = null, i.removeFromArray(o.eleCaches, a), t.checkTextureUtility(o)
                        }
                    }
            }, C.checkTextureUtility = function (e) {
                e.invalidatedWidth >= g * e.width && this.retireTexture(e)
            }, C.checkTextureFullness = function (e) {
                var t = this,
                    r = t.getTextureQueue(e.height);
                e.usedWidth / e.width > y && e.fullnessChecks >= m ? i.removeFromArray(r, e) : e.fullnessChecks++
            }, C.retireTexture = function (e) {
                var t = this,
                    r = e.height,
                    n = t.getTextureQueue(r);
                i.removeFromArray(n, e), e.retired = !0;
                for (var a = e.eleCaches, o = 0; o < a.length; o++) {
                    var s = a[o],
                        l = s.ele,
                        u = s.level,
                        c = l._private.rscratch.imgCaches;
                    c && (c[u] = null)
                }
                i.clearArray(a);
                var d = t.getRetiredTextureQueue(r);
                d.push(e)
            }, C.addTexture = function (e, t) {
                var r = this,
                    n = r.getTextureQueue(e),
                    i = {};
                return n.push(i), i.eleCaches = [], i.height = e, i.width = Math.max(p, t), i.usedWidth = 0, i.invalidatedWidth = 0, i.fullnessChecks = 0, i.canvas = document.createElement("canvas"), i.canvas.width = i.width, i.canvas.height = i.height, i.context = i.canvas.getContext("2d"), i
            }, C.recycleTexture = function (e, t) {
                for (var r = this, n = r.getTextureQueue(e), a = r.getRetiredTextureQueue(e), o = 0; o < a.length; o++) {
                    var s = a[o];
                    if (s.width >= t) return s.retired = !1, s.usedWidth = 0, s.invalidatedWidth = 0, s.fullnessChecks = 0, i.clearArray(s.eleCaches), s.context.clearRect(0, 0, s.width, s.height), i.removeFromArray(a, s), n.push(s), s
                }
            }, C.queueElement = function (e, t, r) {
                var i = this,
                    a = i.getElementQueue(),
                    o = i.getElementIdToQueue(),
                    s = e.id(),
                    l = o[s];
                if (l) l.level = Math.max(l.level, r), l.reqs++ , a.updateItem(l);
                else {
                    var u = {
                        ele: e,
                        bb: t,
                        position: n.copyPosition(e.position()),
                        level: r,
                        reqs: 1
                    };
                    e.isEdge() && (u.positions = {
                        source: n.copyPosition(e.source().position()),
                        target: n.copyPosition(e.target().position())
                    }), a.push(u), o[s] = u
                }
            }, C.dequeue = function (e, t) {
                for (var r = this, i = r.getElementQueue(), a = r.getElementIdToQueue(), o = [], s = 0; T > s && i.size() > 0; s++) {
                    var l = i.pop();
                    a[l.ele.id()] = null, o.push(l);
                    var u, c = l.ele;
                    u = (!c.isEdge() || n.arePositionsSame(c.source().position(), l.positions.source) && n.arePositionsSame(c.target().position(), l.positions.target)) && n.arePositionsSame(c.position(), l.position) ? l.bb : c.boundingBox(), r.getElement(l.ele, u, e, l.level, k.dequeue)
                }
                return o
            }, C.onDequeue = function (e) {
                this.onDequeues.push(e)
            }, C.offDequeue = function (e) {
                i.removeFromArray(this.onDequeues, e)
            }, C.setupDequeueing = o.setupDequeueing({
                deqRedrawThreshold: P,
                deqCost: w,
                deqAvgCost: E,
                deqNoDrawCost: _,
                deqFastCost: S,
                deq: function (e, t, r) {
                    return e.dequeue(t, r)
                },
                onDeqd: function (e, t) {
                    for (var r = 0; r < e.onDequeues.length; r++) {
                        var n = e.onDequeues[r];
                        n(t)
                    }
                },
                shouldRedraw: function (e, t, r, i) {
                    for (var a = 0; a < t.length; a++) {
                        var o = t[a].bb;
                        if (n.boundingBoxesIntersect(o, i)) return !0
                    }
                    return !1
                },
                priority: function (e) {
                    return e.renderer.beforeRenderPriorities.eleTxrDeq
                }
            }), t.exports = D
        }, {
            "../../../heap": 81,
            "../../../math": 85,
            "../../../util": 100,
            "./texture-cache-defs": 77
        }],
        73: [function (e, t, r) {
            "use strict";
            var n = e("../../../is"),
                i = {};
            i.createBuffer = function (e, t) {
                var r = document.createElement("canvas");
                return r.width = e, r.height = t, [r, r.getContext("2d")]
            }, i.bufferCanvasImage = function (e) {
                var t = this.cy,
                    r = t.mutableElements(),
                    i = r.boundingBox(),
                    a = this.findContainerClientCoords(),
                    o = e.full ? Math.ceil(i.w) : a[2],
                    s = e.full ? Math.ceil(i.h) : a[3],
                    l = n.number(e.maxWidth) || n.number(e.maxHeight),
                    u = this.getPixelRatio(),
                    c = 1;
                if (void 0 !== e.scale) o *= e.scale, s *= e.scale, c = e.scale;
                else if (l) {
                    var d = 1 / 0,
                        h = 1 / 0;
                    n.number(e.maxWidth) && (d = c * e.maxWidth / o), n.number(e.maxHeight) && (h = c * e.maxHeight / s), c = Math.min(d, h), o *= c, s *= c
                }
                l || (o *= u, s *= u, c *= u);
                var p = document.createElement("canvas");
                p.width = o, p.height = s, p.style.width = o + "px", p.style.height = s + "px";
                var f = p.getContext("2d");
                if (o > 0 && s > 0) {
                    f.clearRect(0, 0, o, s), e.bg && (f.fillStyle = e.bg, f.rect(0, 0, o, s), f.fill()), f.globalCompositeOperation = "source-over";
                    var v = this.getCachedZSortedEles();
                    if (e.full) f.translate(-i.x1 * c, -i.y1 * c), f.scale(c, c), this.drawElements(f, v);
                    else {
                        var g = t.pan(),
                            y = {
                                x: g.x * c,
                                y: g.y * c
                            };
                        c *= t.zoom(), f.translate(y.x, y.y), f.scale(c, c), this.drawElements(f, v)
                    }
                }
                return p
            }, i.png = function (e) {
                return this.bufferCanvasImage(e).toDataURL("image/png")
            }, i.jpg = function (e) {
                return this.bufferCanvasImage(e).toDataURL("image/jpeg")
            }, t.exports = i
        }, {
            "../../../is": 83
        }],
        74: [function (e, t, r) {
            "use strict";

            function n(e) {
                var t = this;
                t.data = {
                    canvases: new Array(u.CANVAS_LAYERS),
                    contexts: new Array(u.CANVAS_LAYERS),
                    canvasNeedsRedraw: new Array(u.CANVAS_LAYERS),
                    bufferCanvases: new Array(u.BUFFER_COUNT),
                    bufferContexts: new Array(u.CANVAS_LAYERS)
                }, t.data.canvasContainer = document.createElement("div");
                var r = t.data.canvasContainer.style;
                t.data.canvasContainer.setAttribute("style", "-webkit-tap-highlight-color: rgba(0,0,0,0);"), r.position = "relative", r.zIndex = "0", r.overflow = "hidden";
                var n = e.cy.container();
                n.appendChild(t.data.canvasContainer), n.setAttribute("style", (n.getAttribute("style") || "") + "-webkit-tap-highlight-color: rgba(0,0,0,0);");
                for (var i = 0; i < u.CANVAS_LAYERS; i++) {
                    var l = t.data.canvases[i] = document.createElement("canvas");
                    t.data.contexts[i] = l.getContext("2d"), l.setAttribute("style", "-webkit-user-select: none; -moz-user-select: -moz-none; user-select: none; -webkit-tap-highlight-color: rgba(0,0,0,0); outline-style: none;" + (a.ms() ? " -ms-touch-action: none; touch-action: none; " : "")), l.style.position = "absolute", l.setAttribute("data-id", "layer" + i), l.style.zIndex = String(u.CANVAS_LAYERS - i), t.data.canvasContainer.appendChild(l), t.data.canvasNeedsRedraw[i] = !1
                }
                t.data.topCanvas = t.data.canvases[0], t.data.canvases[u.NODE].setAttribute("data-id", "layer" + u.NODE + "-node"), t.data.canvases[u.SELECT_BOX].setAttribute("data-id", "layer" + u.SELECT_BOX + "-selectbox"), t.data.canvases[u.DRAG].setAttribute("data-id", "layer" + u.DRAG + "-drag");
                for (var i = 0; i < u.BUFFER_COUNT; i++) t.data.bufferCanvases[i] = document.createElement("canvas"), t.data.bufferContexts[i] = t.data.bufferCanvases[i].getContext("2d"), t.data.bufferCanvases[i].style.position = "absolute", t.data.bufferCanvases[i].setAttribute("data-id", "buffer" + i), t.data.bufferCanvases[i].style.zIndex = String(-i - 1), t.data.bufferCanvases[i].style.visibility = "hidden";
                t.pathsEnabled = !0, t.data.eleTxrCache = new o(t), t.data.lyrTxrCache = new s(t, t.data.eleTxrCache), t.onUpdateEleCalcs(function (e, r) {
                    for (var n = 0; n < r.length; n++) {
                        var i = r[n],
                            a = i._private.rstyle,
                            o = a.dirtyEvents;
                        i.isNode() && o && 1 === o.length && o.position || t.data.eleTxrCache.invalidateElement(i)
                    }
                    r.length > 0 && t.data.lyrTxrCache.invalidateElements(r)
                })
            }
            var i = e("../../../util"),
                a = e("../../../is"),
                o = e("./ele-texture-cache"),
                s = e("./layered-texture-cache"),
                l = n,
                u = n.prototype;
            u.CANVAS_LAYERS = 3, u.SELECT_BOX = 0, u.DRAG = 1, u.NODE = 2, u.BUFFER_COUNT = 3, u.TEXTURE_BUFFER = 0, u.MOTIONBLUR_BUFFER_NODE = 1, u.MOTIONBLUR_BUFFER_DRAG = 2, u.redrawHint = function (e, t) {
                var r = this;
                switch (e) {
                    case "eles":
                        r.data.canvasNeedsRedraw[u.NODE] = t;
                        break;
                    case "drag":
                        r.data.canvasNeedsRedraw[u.DRAG] = t;
                        break;
                    case "select":
                        r.data.canvasNeedsRedraw[u.SELECT_BOX] = t
                }
            };
            var c = "undefined" != typeof Path2D;
            u.path2dEnabled = function (e) {
                return void 0 === e ? this.pathsEnabled : void (this.pathsEnabled = !!e)
            }, u.usePaths = function () {
                return c && this.pathsEnabled
            }, [e("./arrow-shapes"), e("./drawing-elements"), e("./drawing-edges"), e("./drawing-images"), e("./drawing-label-text"), e("./drawing-nodes"), e("./drawing-redraw"), e("./drawing-shapes"), e("./export-image"), e("./node-shapes")].forEach(function (e) {
                i.extend(u, e)
            }), t.exports = l
        }, {
            "../../../is": 83,
            "../../../util": 100,
            "./arrow-shapes": 64,
            "./drawing-edges": 65,
            "./drawing-elements": 66,
            "./drawing-images": 67,
            "./drawing-label-text": 68,
            "./drawing-nodes": 69,
            "./drawing-redraw": 70,
            "./drawing-shapes": 71,
            "./ele-texture-cache": 72,
            "./export-image": 73,
            "./layered-texture-cache": 75,
            "./node-shapes": 76
        }],
        75: [function (e, t, r) {
            "use strict";

            function n(e, t) {
                null != e.imageSmoothingEnabled ? e.imageSmoothingEnabled = t : (e.webkitImageSmoothingEnabled = t, e.mozImageSmoothingEnabled = t, e.msImageSmoothingEnabled = t)
            }
            var i = e("../../../util"),
                a = e("../../../math"),
                o = e("../../../heap"),
                s = e("../../../is"),
                l = e("./texture-cache-defs"),
                u = 1,
                c = -4,
                d = 2,
                h = 3.99,
                p = 50,
                f = 50,
                v = !0,
                g = .15,
                y = .1,
                m = .9,
                b = .9,
                x = 1,
                w = 250,
                E = 16e6,
                _ = !0,
                S = !0,
                P = !0,
                T = function (e, t) {
                    var r = this,
                        n = r.renderer = e;
                    r.layersByLevel = {}, r.firstGet = !0, r.lastInvalidationTime = i.performanceNow() - 2 * w, r.skipping = !1, n.beforeRender(function (e, t) {
                        t - r.lastInvalidationTime <= w ? r.skipping = !0 : r.skipping = !1
                    });
                    var a = function (e, t) {
                        return t.reqs - e.reqs
                    };
                    r.layersQueue = new o(a), r.eleTxrCache = t, r.setupEleCacheInvalidation(), r.setupDequeueing()
                },
                k = T.prototype,
                D = 0,
                C = Math.pow(2, 53) - 1;
            k.makeLayer = function (e, t) {
                var r = Math.pow(2, t),
                    n = Math.ceil(e.w * r),
                    i = Math.ceil(e.h * r),
                    a = document.createElement("canvas");
                a.width = n, a.height = i;
                var o = {
                    id: D = ++D % C,
                    bb: e,
                    level: t,
                    width: n,
                    height: i,
                    canvas: a,
                    context: a.getContext("2d"),
                    eles: [],
                    elesQueue: [],
                    reqs: 0
                },
                    s = o.context,
                    l = -o.bb.x1,
                    u = -o.bb.y1;
                return s.scale(r, r), s.translate(l, u), o
            }, k.getLayers = function (e, t, r) {
                var n = this,
                    o = n.renderer,
                    s = o.cy,
                    l = s.zoom(),
                    p = n.firstGet;
                if (n.firstGet = !1, null == r)
                    if (r = Math.ceil(a.log2(l * t)), c > r) r = c;
                    else if (l >= h || r > d) return null;
                n.validateLayersElesOrdering(r, e);
                var f, v, g = n.layersByLevel,
                    y = Math.pow(2, r),
                    m = g[r] = g[r] || [],
                    b = n.levelIsComplete(r, e),
                    x = function () {
                        var t = function (t) {
                            return n.validateLayersElesOrdering(t, e), n.levelIsComplete(t, e) ? (v = g[t], !0) : void 0
                        },
                            a = function (e) {
                                if (!v)
                                    for (var n = r + e; n >= c && d >= n && !t(n); n += e);
                            };
                        a(1), a(-1);
                        for (var o = m.length - 1; o >= 0; o--) {
                            var s = m[o];
                            s.invalid && i.removeFromArray(m, s)
                        }
                    };
                if (b) return m;
                x();
                var w = function () {
                    if (!f) {
                        f = a.makeBoundingBox();
                        for (var t = 0; t < e.length; t++) a.updateBoundingBox(f, e[t].boundingBox())
                    }
                    return f
                },
                    S = function (e) {
                        e = e || {};
                        var t = e.after;
                        w();
                        var i = f.w * y * (f.h * y);
                        if (i > E) return null;
                        var a = n.makeLayer(f, r);
                        if (null != t) {
                            var o = m.indexOf(t) + 1;
                            m.splice(o, 0, a)
                        } else (void 0 === e.insert || e.insert) && m.unshift(a);
                        return a
                    };
                if (n.skipping && !p) return null;
                for (var P = null, T = e.length / u, k = _ && !p, D = 0; D < e.length; D++) {
                    var C = e[D],
                        M = C._private.rscratch,
                        N = M.imgLayerCaches = M.imgLayerCaches || {},
                        B = N[r];
                    if (B) P = B;
                    else {
                        if ((!P || P.eles.length >= T || !a.boundingBoxInBoundingBox(P.bb, C.boundingBox())) && (P = S({
                            insert: !0,
                            after: P
                        }), !P)) return null;
                        v || k ? n.queueLayer(P, C) : n.drawEleInLayer(P, C, r, t), P.eles.push(C), N[r] = P
                    }
                }
                return v ? v : k ? null : m
            }, k.getEleLevelForLayerLevel = function (e, t) {
                return e
            }, k.drawEleInLayer = function (e, t, r, i) {
                var a = this,
                    o = this.renderer,
                    s = e.context,
                    l = t.boundingBox();
                if (0 !== l.w && 0 !== l.h) {
                    var u = a.eleTxrCache,
                        c = S ? u.reasons.highQuality : void 0;
                    r = a.getEleLevelForLayerLevel(r, i);
                    var d = P ? u.getElement(t, l, null, r, c) : null;
                    d ? (v && n(s, !1), s.drawImage(d.texture.canvas, d.x, 0, d.width, d.height, l.x1, l.y1, l.w, l.h), v && n(s, !0)) : o.drawElement(s, t)
                }
            }, k.levelIsComplete = function (e, t) {
                var r = this,
                    n = r.layersByLevel[e];
                if (!n || 0 === n.length) return !1;
                for (var i = 0, a = 0; a < n.length; a++) {
                    var o = n[a];
                    if (o.reqs > 0) return !1;
                    if (o.invalid) return !1;
                    i += o.eles.length
                }
                return i === t.length
            }, k.validateLayersElesOrdering = function (e, t) {
                var r = this.layersByLevel[e];
                if (r)
                    for (var n = 0; n < r.length; n++) {
                        for (var i = r[n], a = -1, o = 0; o < t.length; o++)
                            if (i.eles[0] === t[o]) {
                                a = o;
                                break
                            }
                        if (0 > a) this.invalidateLayer(i);
                        else
                            for (var s = a, o = 0; o < i.eles.length; o++)
                                if (i.eles[o] !== t[s + o]) {
                                    this.invalidateLayer(i);
                                    break
                                }
                    }
            }, k.updateElementsInLayers = function (e, t) {
                for (var r = this, n = s.element(e[0]), i = 0; i < e.length; i++)
                    for (var a = n ? null : e[i], o = n ? e[i] : e[i].ele, l = o._private.rscratch, u = l.imgLayerCaches = l.imgLayerCaches || {}, h = c; d >= h; h++) {
                        var p = u[h];
                        p && (a && r.getEleLevelForLayerLevel(p.level) !== a.level || t(p, o, a))
                    }
            }, k.haveLayers = function () {
                for (var e = this, t = !1, r = c; d >= r; r++) {
                    var n = e.layersByLevel[r];
                    if (n && n.length > 0) {
                        t = !0;
                        break
                    }
                }
                return t
            }, k.invalidateElements = function (e) {
                var t = this;
                t.lastInvalidationTime = i.performanceNow(), 0 !== e.length && t.haveLayers() && t.updateElementsInLayers(e, function (e, r, n) {
                    t.invalidateLayer(e)
                })
            }, k.invalidateLayer = function (e) {
                if (this.lastInvalidationTime = i.performanceNow(), !e.invalid) {
                    var t = e.level,
                        r = e.eles,
                        n = this.layersByLevel[t];
                    i.removeFromArray(n, e), e.elesQueue = [], e.invalid = !0, e.replacement && (e.replacement.invalid = !0);
                    for (var a = 0; a < r.length; a++) {
                        var o = r[a]._private.rscratch.imgLayerCaches;
                        o && (o[t] = null)
                    }
                }
            }, k.refineElementTextures = function (e) {
                var t = this;
                t.updateElementsInLayers(e, function (e, r, n) {
                    var i = e.replacement;
                    if (i || (i = e.replacement = t.makeLayer(e.bb, e.level), i.replaces = e, i.eles = e.eles), !i.reqs)
                        for (var a = 0; a < i.eles.length; a++) t.queueLayer(i, i.eles[a])
                })
            }, k.setupEleCacheInvalidation = function () {
                var e = this,
                    t = [];
                if (P) {
                    var r = i.debounce(function () {
                        e.refineElementTextures(t), t = []
                    }, f);
                    e.eleTxrCache.onDequeue(function (e) {
                        for (var n = 0; n < e.length; n++) t.push(e[n]);
                        r()
                    })
                }
            }, k.queueLayer = function (e, t) {
                var r = this,
                    n = r.layersQueue,
                    i = e.elesQueue,
                    a = i.hasId = i.hasId || {};
                if (!e.replacement) {
                    if (t) {
                        if (a[t.id()]) return;
                        i.push(t), a[t.id()] = !0
                    }
                    e.reqs ? (e.reqs++ , n.updateItem(e)) : (e.reqs = 1, n.push(e))
                }
            }, k.dequeue = function (e) {
                for (var t = this, r = t.layersQueue, n = [], i = 0; x > i && 0 !== r.size();) {
                    var a = r.peek();
                    if (a.replacement) r.pop();
                    else if (a.replaces && a !== a.replaces.replacement) r.pop();
                    else if (a.invalid) r.pop();
                    else {
                        var o = a.elesQueue.shift();
                        o && (t.drawEleInLayer(a, o, a.level, e), i++), 0 === n.length && n.push(!0), 0 === a.elesQueue.length && (r.pop(), a.reqs = 0, a.replaces && t.applyLayerReplacement(a), t.requestRedraw())
                    }
                }
                return n
            }, k.applyLayerReplacement = function (e) {
                var t = this,
                    r = t.layersByLevel[e.level],
                    n = e.replaces,
                    i = r.indexOf(n);
                if (!(0 > i || n.invalid)) {
                    r[i] = e;
                    for (var a = 0; a < e.eles.length; a++) {
                        var o = e.eles[a]._private,
                            s = o.imgLayerCaches = o.imgLayerCaches || {};
                        s && (s[e.level] = e)
                    }
                    t.requestRedraw()
                }
            }, k.requestRedraw = i.debounce(function () {
                var e = this.renderer;
                e.redrawHint("eles", !0), e.redrawHint("drag", !0), e.redraw()
            }, 100), k.setupDequeueing = l.setupDequeueing({
                deqRedrawThreshold: p,
                deqCost: g,
                deqAvgCost: y,
                deqNoDrawCost: m,
                deqFastCost: b,
                deq: function (e, t) {
                    return e.dequeue(t)
                },
                onDeqd: i.noop,
                shouldRedraw: i.trueify,
                priority: function (e) {
                    return e.renderer.beforeRenderPriorities.lyrTxrDeq
                }
            }), t.exports = T
        }, {
            "../../../heap": 81,
            "../../../is": 83,
            "../../../math": 85,
            "../../../util": 100,
            "./texture-cache-defs": 77
        }],
        76: [function (e, t, r) {
            "use strict";
            var n = {};
            n.nodeShapeImpl = function (e, t, r, n, i, a, o) {
                switch (e) {
                    case "ellipse":
                        return this.drawEllipsePath(t, r, n, i, a);
                    case "polygon":
                        return this.drawPolygonPath(t, r, n, i, a, o);
                    case "roundrectangle":
                        return this.drawRoundRectanglePath(t, r, n, i, a)
                }
            }, t.exports = n
        }, {}],
        77: [function (e, t, r) {
            "use strict";
            var n = e("../../../util"),
                i = 1e3 / 60;
            t.exports = {
                setupDequeueing: function (e) {
                    return function () {
                        var t = this,
                            r = this.renderer;
                        if (!t.dequeueingSetup) {
                            t.dequeueingSetup = !0;
                            var a = n.debounce(function () {
                                r.redrawHint("eles", !0), r.redrawHint("drag", !0), r.redraw()
                            }, e.deqRedrawThreshold),
                                o = function (o, s) {
                                    for (var l = n.performanceNow(), u = r.averageRedrawTime, c = r.lastRedrawTime, d = [], h = r.cy.extent(), p = r.getPixelRatio(); ;) {
                                        var f = n.performanceNow(),
                                            v = f - l,
                                            g = f - s;
                                        if (i > c) {
                                            var y = i - (o ? u : 0);
                                            if (g >= e.deqFastCost * y) break
                                        } else if (o) {
                                            if (v >= e.deqCost * c || v >= e.deqAvgCost * u) break
                                        } else if (g >= e.deqNoDrawCost * i) break;
                                        var m = e.deq(t, p, h);
                                        if (!(m.length > 0)) break;
                                        for (var b = 0; b < m.length; b++) d.push(m[b])
                                    }
                                    d.length > 0 && (e.onDeqd(t, d), !o && e.shouldRedraw(t, d, p, h) && a())
                                },
                                s = e.priority || n.noop;
                            r.beforeRender(o, s(t))
                        }
                    }
                }
            }
        }, {
            "../../../util": 100
        }],
        78: [function (e, t, r) {
            "use strict";
            t.exports = [{
                name: "null",
                impl: e("./null")
            }, {
                name: "base",
                impl: e("./base")
            }, {
                name: "canvas",
                impl: e("./canvas")
            }]
        }, {
            "./base": 60,
            "./canvas": 74,
            "./null": 79
        }],
        79: [function (e, t, r) {
            "use strict";

            function n(e) {
                this.options = e, this.notifications = 0
            }
            var i = function () { };
            n.prototype = {
                recalculateRenderedStyle: i,
                notify: function () {
                    this.notifications++
                },
                init: i
            }, t.exports = n
        }, {}],
        80: [function (e, t, r) { /*! Weaver licensed under MIT (https://tldrlegal.com/license/mit-license), copyright Max Franz */
            "use strict";
            var n = e("./is"),
                i = e("./util"),
                a = e("./thread"),
                o = e("./promise"),
                s = e("./define"),
                l = function (t) {
                    if (!(this instanceof l)) return new l(t);
                    this._private = {
                        pass: []
                    };
                    var r = 4;
                    if (n.number(t), "undefined" != typeof navigator && null != navigator.hardwareConcurrency) t = navigator.hardwareConcurrency;
                    else try {
                        t = e("os").cpus().length
                    } catch (i) {
                        t = r
                    }
                    for (var o = 0; t > o; o++) this[o] = new a;
                    this.length = t
                },
                u = l.prototype;
            i.extend(u, {
                instanceString: function () {
                    return "fabric"
                },
                require: function (e, t) {
                    for (var r = 0; r < this.length; r++) {
                        var n = this[r];
                        n.require(e, t)
                    }
                    return this
                },
                random: function () {
                    var e = Math.round((this.length - 1) * Math.random()),
                        t = this[e];
                    return t
                },
                run: function (e) {
                    var t = this._private.pass.shift();
                    return this.random().pass(t).run(e)
                },
                message: function (e) {
                    return this.random().message(e)
                },
                broadcast: function (e) {
                    for (var t = 0; t < this.length; t++) {
                        var r = this[t];
                        r.message(e)
                    }
                    return this
                },
                stop: function () {
                    for (var e = 0; e < this.length; e++) {
                        var t = this[e];
                        t.stop()
                    }
                    return this
                },
                pass: function (e) {
                    var t = this._private.pass;
                    if (!n.array(e)) throw "Only arrays may be used with fabric.pass()";
                    return t.push(e), this
                },
                spreadSize: function () {
                    var e = Math.ceil(this._private.pass[0].length / this.length);
                    return e = Math.max(1, e)
                },
                spread: function (e) {
                    for (var t = this, r = t._private, n = t.spreadSize(), i = r.pass.shift().concat([]), a = [], s = 0; s < this.length; s++) {
                        var l = this[s],
                            u = i.splice(0, n),
                            c = l.pass(u).run(e);
                        a.push(c);
                        var d = 0 === i.length;
                        if (d) break
                    }
                    return o.all(a).then(function (e) {
                        for (var t = [], r = 0, n = 0; n < e.length; n++)
                            for (var i = e[n], a = 0; a < i.length; a++) {
                                var o = i[a];
                                t[r++] = o
                            }
                        return t
                    })
                },
                map: function (e) {
                    var t = this;
                    return t.require(e, "_$_$_fabmap"), t.spread(function (e) {
                        var t = [],
                            r = resolve;
                        resolve = function (e) {
                            t.push(e)
                        };
                        for (var n = 0; n < e.length; n++) {
                            var i = t.length,
                                a = _$_$_fabmap(e[n]),
                                o = i === t.length;
                            o && t.push(a)
                        }
                        return resolve = r, t
                    })
                },
                filter: function (e) {
                    var t = this._private,
                        r = t.pass[0];
                    return this.map(e).then(function (e) {
                        for (var t = [], n = 0; n < r.length; n++) {
                            var i = r[n],
                                a = e[n];
                            a && t.push(i)
                        }
                        return t
                    })
                },
                sort: function (e) {
                    var t = this,
                        r = this._private.pass[0].length,
                        n = this.spreadSize();
                    return e = e || function (e, t) {
                        return t > e ? -1 : e > t ? 1 : 0
                    }, t.require(e, "_$_$_cmp"), t.spread(function (e) {
                        var t = e.sort(_$_$_cmp);
                        resolve(t)
                    }).then(function (t) {
                        for (var i = function (n, i, a) {
                            i = Math.min(i, r), a = Math.min(a, r);
                            for (var o = n, s = i, l = [], u = o; a > u; u++) {
                                var c = t[n],
                                    d = t[i];
                                s > n && (i >= a || e(c, d) <= 0) ? (l.push(c), n++) : (l.push(d), i++)
                            }
                            for (var u = 0; u < l.length; u++) {
                                var h = o + u;
                                t[h] = l[u]
                            }
                        }, a = n; r > a; a *= 2)
                            for (var o = 0; r > o; o += 2 * a) i(o, o + a, o + 2 * a);
                        return t
                    })
                }
            });
            var c = function (e) {
                return e = e || {},
                    function (t, r) {
                        var n = this._private.pass.shift();
                        return this.random().pass(n)[e.threadFn](t, r)
                    }
            };
            i.extend(u, {
                randomMap: c({
                    threadFn: "map"
                }),
                reduce: c({
                    threadFn: "reduce"
                }),
                reduceRight: c({
                    threadFn: "reduceRight"
                })
            });
            var d = u;
            d.promise = d.run, d.terminate = d.halt = d.stop, d.include = d.require, i.extend(u, {
                on: s.on(),
                one: s.on({
                    unbindSelfOnTrigger: !0
                }),
                off: s.off(),
                trigger: s.trigger()
            }), s.eventAliasesOn(u), t.exports = l
        }, {
            "./define": 44,
            "./is": 83,
            "./promise": 86,
            "./thread": 98,
            "./util": 100,
            os: void 0
        }],
        81: [function (e, t, r) {
            /*!
            Ported by Xueqiao Xu <xueqiaoxu@gmail.com>;

            PSF LICENSE AGREEMENT FOR PYTHON 2.7.2

            1. This LICENSE AGREEMENT is between the Python Software Foundation (“PSF”), and the Individual or Organization (“Licensee”) accessing and otherwise using Python 2.7.2 software in source or binary form and its associated documentation.
            2. Subject to the terms and conditions of this License Agreement, PSF hereby grants Licensee a nonexclusive, royalty-free, world-wide license to reproduce, analyze, test, perform and/or display publicly, prepare derivative works, distribute, and otherwise use Python 2.7.2 alone or in any derivative version, provided, however, that PSF’s License Agreement and PSF’s notice of copyright, i.e., “Copyright © 2001-2012 Python Software Foundation; All Rights Reserved” are retained in Python 2.7.2 alone or in any derivative version prepared by Licensee.
            3. In the event Licensee prepares a derivative work that is based on or incorporates Python 2.7.2 or any part thereof, and wants to make the derivative work available to others as provided herein, then Licensee hereby agrees to include in any such work a brief summary of the changes made to Python 2.7.2.
            4. PSF is making Python 2.7.2 available to Licensee on an “AS IS” basis. PSF MAKES NO REPRESENTATIONS OR WARRANTIES, EXPRESS OR IMPLIED. BY WAY OF EXAMPLE, BUT NOT LIMITATION, PSF MAKES NO AND DISCLAIMS ANY REPRESENTATION OR WARRANTY OF MERCHANTABILITY OR FITNESS FOR ANY PARTICULAR PURPOSE OR THAT THE USE OF PYTHON 2.7.2 WILL NOT INFRINGE ANY THIRD PARTY RIGHTS.
            5. PSF SHALL NOT BE LIABLE TO LICENSEE OR ANY OTHER USERS OF PYTHON 2.7.2 FOR ANY INCIDENTAL, SPECIAL, OR CONSEQUENTIAL DAMAGES OR LOSS AS A RESULT OF MODIFYING, DISTRIBUTING, OR OTHERWISE USING PYTHON 2.7.2, OR ANY DERIVATIVE THEREOF, EVEN IF ADVISED OF THE POSSIBILITY THEREOF.
            6. This License Agreement will automatically terminate upon a material breach of its terms and conditions.
            7. Nothing in this License Agreement shall be deemed to create any relationship of agency, partnership, or joint venture between PSF and Licensee. This License Agreement does not grant permission to use PSF trademarks or trade name in a trademark sense to endorse or promote products or services of Licensee, or any third party.
            8. By copying, installing or otherwise using Python 2.7.2, Licensee agrees to be bound by the terms and conditions of this License Agreement.
            */
            "use strict";
            var n, i, a, o, s, l, u, c, d, h, p, f, v, g, y;
            a = Math.floor, h = Math.min, i = function (e, t) {
                return t > e ? -1 : e > t ? 1 : 0
            }, d = function (e, t, r, n, o) {
                var s;
                if (null == r && (r = 0), null == o && (o = i), 0 > r) throw new Error("lo must be non-negative");
                for (null == n && (n = e.length); n > r;) s = a((r + n) / 2), o(t, e[s]) < 0 ? n = s : r = s + 1;
                return [].splice.apply(e, [r, r - r].concat(t)), t
            }, l = function (e, t, r) {
                return null == r && (r = i), e.push(t), g(e, 0, e.length - 1, r)
            }, s = function (e, t) {
                var r, n;
                return null == t && (t = i), r = e.pop(), e.length ? (n = e[0], e[0] = r, y(e, 0, t)) : n = r, n
            }, c = function (e, t, r) {
                var n;
                return null == r && (r = i), n = e[0], e[0] = t, y(e, 0, r), n
            }, u = function (e, t, r) {
                var n;
                return null == r && (r = i), e.length && r(e[0], t) < 0 && (n = [e[0], t], t = n[0], e[0] = n[1], y(e, 0, r)), t
            }, o = function (e, t) {
                var r, n, o, s, l, u;
                for (null == t && (t = i), s = function () {
                    u = [];
                    for (var t = 0, r = a(e.length / 2); r >= 0 ? r > t : t > r; r >= 0 ? t++ : t--) u.push(t);
                    return u
                }.apply(this).reverse(), l = [], n = 0, o = s.length; o > n; n++) r = s[n], l.push(y(e, r, t));
                return l
            }, v = function (e, t, r) {
                var n;
                return null == r && (r = i), n = e.indexOf(t), -1 !== n ? (g(e, 0, n, r), y(e, n, r)) : void 0
            }, p = function (e, t, r) {
                var n, a, s, l, c;
                if (null == r && (r = i), a = e.slice(0, t), !a.length) return a;
                for (o(a, r), c = e.slice(t), s = 0, l = c.length; l > s; s++) n = c[s], u(a, n, r);
                return a.sort(r).reverse()
            }, f = function (e, t, r) {
                var n, a, l, u, c, p, f, v, g, y;
                if (null == r && (r = i), 10 * t <= e.length) {
                    if (u = e.slice(0, t).sort(r), !u.length) return u;
                    for (l = u[u.length - 1], v = e.slice(t), c = 0, f = v.length; f > c; c++) n = v[c], r(n, l) < 0 && (d(u, n, 0, null, r), u.pop(), l = u[u.length - 1]);
                    return u
                }
                for (o(e, r), y = [], a = p = 0, g = h(t, e.length); g >= 0 ? g > p : p > g; a = g >= 0 ? ++p : --p) y.push(s(e, r));
                return y
            }, g = function (e, t, r, n) {
                var a, o, s;
                for (null == n && (n = i), a = e[r]; r > t && (s = r - 1 >> 1, o = e[s], n(a, o) < 0);) e[r] = o, r = s;
                return e[r] = a
            }, y = function (e, t, r) {
                var n, a, o, s, l;
                for (null == r && (r = i), a = e.length, l = t, o = e[t], n = 2 * t + 1; a > n;) s = n + 1, a > s && !(r(e[n], e[s]) < 0) && (n = s), e[t] = e[n], t = n, n = 2 * t + 1;
                return e[t] = o, g(e, l, t, r)
            }, n = function () {
                function e(e) {
                    this.cmp = null != e ? e : i, this.nodes = []
                }
                return e.push = l, e.pop = s, e.replace = c, e.pushpop = u, e.heapify = o, e.updateItem = v, e.nlargest = p, e.nsmallest = f, e.prototype.push = function (e) {
                    return l(this.nodes, e, this.cmp)
                }, e.prototype.pop = function () {
                    return s(this.nodes, this.cmp)
                }, e.prototype.peek = function () {
                    return this.nodes[0]
                }, e.prototype.contains = function (e) {
                    return -1 !== this.nodes.indexOf(e)
                }, e.prototype.replace = function (e) {
                    return c(this.nodes, e, this.cmp)
                }, e.prototype.pushpop = function (e) {
                    return u(this.nodes, e, this.cmp)
                }, e.prototype.heapify = function () {
                    return o(this.nodes, this.cmp)
                }, e.prototype.updateItem = function (e) {
                    return v(this.nodes, e, this.cmp)
                }, e.prototype.clear = function () {
                    return this.nodes = []
                }, e.prototype.empty = function () {
                    return 0 === this.nodes.length
                }, e.prototype.size = function () {
                    return this.nodes.length
                }, e.prototype.clone = function () {
                    var t;
                    return t = new e, t.nodes = this.nodes.slice(0), t
                }, e.prototype.toArray = function () {
                    return this.nodes.slice(0)
                }, e.prototype.insert = e.prototype.push, e.prototype.top = e.prototype.peek, e.prototype.front = e.prototype.peek, e.prototype.has = e.prototype.contains, e.prototype.copy = e.prototype.clone, e
            }(), t.exports = n
        }, {}],
        82: [function (e, t, r) {
            "use strict";
            e("./-preamble");
            var n = e("./window"),
                i = e("./is"),
                a = e("./core"),
                o = e("./extension"),
                s = e("./jquery-plugin"),
                l = e("./stylesheet"),
                u = e("./thread"),
                c = e("./fabric"),
                d = function (e) {
                    return void 0 === e && (e = {}), i.plainObject(e) ? new a(e) : i.string(e) ? o.apply(o, arguments) : void 0
                };
            d.version = e("./version"), n && n.jQuery && s(n.jQuery, d), d.registerJquery = function (e) {
                s(e, d)
            }, d.stylesheet = d.Stylesheet = l, d.thread = d.Thread = u, d.fabric = d.Fabric = c, t.exports = d
        }, {
            "./-preamble": 1,
            "./core": 37,
            "./extension": 46,
            "./fabric": 80,
            "./is": 83,
            "./jquery-plugin": 84,
            "./stylesheet": 97,
            "./thread": 98,
            "./version": 106,
            "./window": 107
        }],
        83: [function (e, t, r) {
            "use strict";
            var n = e("./window"),
                i = n ? n.navigator : null,
                a = n ? n.document : null,
                o = "string",
                s = typeof {},
                l = "function",
                u = typeof HTMLElement,
                c = function (e) {
                    return e && e.instanceString && d.fn(e.instanceString) ? e.instanceString() : null
                },
                d = {
                    defined: function (e) {
                        return null != e
                    },
                    string: function (e) {
                        return null != e && typeof e == o
                    },
                    fn: function (e) {
                        return null != e && typeof e === l
                    },
                    array: function (e) {
                        return Array.isArray ? Array.isArray(e) : null != e && e instanceof Array
                    },
                    plainObject: function (e) {
                        return null != e && typeof e === s && !d.array(e) && e.constructor === Object
                    },
                    object: function (e) {
                        return null != e && typeof e === s
                    },
                    number: function (e) {
                        return null != e && "number" == typeof e && !isNaN(e)
                    },
                    integer: function (e) {
                        return d.number(e) && Math.floor(e) === e
                    },
                    bool: function (e) {
                        return null != e && typeof e == typeof !0
                    },
                    htmlElement: function (e) {
                        return "undefined" === u ? void 0 : null != e && e instanceof HTMLElement
                    },
                    elementOrCollection: function (e) {
                        return d.element(e) || d.collection(e)
                    },
                    element: function (e) {
                        return "collection" === c(e) && e._private.single
                    },
                    collection: function (e) {
                        return "collection" === c(e) && !e._private.single
                    },
                    core: function (e) {
                        return "core" === c(e)
                    },
                    style: function (e) {
                        return "style" === c(e)
                    },
                    stylesheet: function (e) {
                        return "stylesheet" === c(e)
                    },
                    event: function (e) {
                        return "event" === c(e)
                    },
                    thread: function (e) {
                        return "thread" === c(e)
                    },
                    fabric: function (e) {
                        return "fabric" === c(e)
                    },
                    emptyString: function (e) {
                        return void 0 === e || null === e ? !0 : !("" !== e && !e.match(/^\s+$/))
                    },
                    nonemptyString: function (e) {
                        return !(!e || !d.string(e) || "" === e || e.match(/^\s+$/))
                    },
                    domElement: function (e) {
                        return "undefined" == typeof HTMLElement ? !1 : e instanceof HTMLElement
                    },
                    boundingBox: function (e) {
                        return d.plainObject(e) && d.number(e.x1) && d.number(e.x2) && d.number(e.y1) && d.number(e.y2)
                    },
                    promise: function (e) {
                        return d.object(e) && d.fn(e.then)
                    },
                    touch: function () {
                        return n && ("ontouchstart" in n || n.DocumentTouch && a instanceof DocumentTouch)
                    },
                    gecko: function () {
                        return n && ("undefined" != typeof InstallTrigger || "MozAppearance" in a.documentElement.style)
                    },
                    webkit: function () {
                        return n && ("undefined" != typeof webkitURL || "WebkitAppearance" in a.documentElement.style)
                    },
                    chromium: function () {
                        return n && "undefined" != typeof chrome
                    },
                    khtml: function () {
                        return i && i.vendor.match(/kde/i)
                    },
                    khtmlEtc: function () {
                        return d.khtml() || d.webkit() || d.chromium()
                    },
                    ms: function () {
                        return i && i.userAgent.match(/msie|trident|edge/i)
                    },
                    windows: function () {
                        return i && i.appVersion.match(/Win/i)
                    },
                    mac: function () {
                        return i && i.appVersion.match(/Mac/i)
                    },
                    linux: function () {
                        return i && i.appVersion.match(/Linux/i)
                    },
                    unix: function () {
                        return i && i.appVersion.match(/X11/i)
                    }
                };
            t.exports = d
        }, {
            "./window": 107
        }],
        84: [function (e, t, r) {
            "use strict";
            var n = e("./is"),
                i = function (e) {
                    var t = e[0]._cyreg = e[0]._cyreg || {};
                    return t
                },
                a = function (e, t) {
                    e && (e.fn.cytoscape || (e.fn.cytoscape = function (r) {
                        var a = e(this);
                        if ("get" === r) return i(a).cy;
                        if (n.fn(r)) {
                            var o = r,
                                s = i(a).cy;
                            if (s && s.isReady()) s.trigger("ready", [], o);
                            else {
                                var l = i(a),
                                    u = l.readies = l.readies || [];
                                u.push(o)
                            }
                        } else if (n.plainObject(r)) return a.each(function () {
                            var n = e.extend({}, r, {
                                container: e(this)[0]
                            });
                            t(n)
                        })
                    }, e.cytoscape = t, null == e.fn.cy && null == e.cy && (e.fn.cy = e.fn.cytoscape, e.cy = e.cytoscape)))
                };
            t.exports = a
        }, {
            "./is": 83
        }],
        85: [function (e, t, r) {
            "use strict";
            var n = {};
            n.arePositionsSame = function (e, t) {
                return e.x === t.x && e.y === t.y
            }, n.copyPosition = function (e) {
                return {
                    x: e.x,
                    y: e.y
                }
            }, n.array2point = function (e) {
                return {
                    x: e[0],
                    y: e[1]
                }
            }, n.deg2rad = function (e) {
                return Math.PI * e / 180
            }, n.log2 = Math.log2 || function (e) {
                return Math.log(e) / Math.log(2)
            }, n.signum = function (e) {
                return e > 0 ? 1 : 0 > e ? -1 : 0
            }, n.dist = function (e, t) {
                return Math.sqrt(n.sqdist(e, t))
            }, n.sqdist = function (e, t) {
                var r = t.x - e.x,
                    n = t.y - e.y;
                return r * r + n * n
            }, n.qbezierAt = function (e, t, r, n) {
                return (1 - n) * (1 - n) * e + 2 * (1 - n) * n * t + n * n * r
            }, n.qbezierPtAt = function (e, t, r, i) {
                return {
                    x: n.qbezierAt(e.x, t.x, r.x, i),
                    y: n.qbezierAt(e.y, t.y, r.y, i)
                }
            }, n.lineAt = function (e, t, r, i) {
                var a = {
                    x: t.x - e.x,
                    y: t.y - e.y
                },
                    o = n.dist(e, t),
                    s = {
                        x: a.x / o,
                        y: a.y / o
                    };
                r = null == r ? 0 : r;
                var i = null != i ? i : r * o;
                return {
                    x: e.x + s.x * i,
                    y: e.y + s.y * i
                }
            }, n.lineAtDist = function (e, t, r) {
                return n.lineAt(e, t, void 0, r)
            }, n.triangleAngle = function (e, t, r) {
                var i = n.dist(t, r),
                    a = n.dist(e, r),
                    o = n.dist(e, t);
                return Math.acos((i * i + a * a - o * o) / (2 * i * a))
            }, n.bound = function (e, t, r) {
                return Math.max(e, Math.min(r, t))
            }, n.makeBoundingBox = function (e) {
                if (null == e) return {
                    x1: 1 / 0,
                    y1: 1 / 0,
                    x2: -(1 / 0),
                    y2: -(1 / 0),
                    w: 0,
                    h: 0
                };
                if (null != e.x1 && null != e.y1) {
                    if (null != e.x2 && null != e.y2 && e.x2 >= e.x1 && e.y2 >= e.y1) return {
                        x1: e.x1,
                        y1: e.y1,
                        x2: e.x2,
                        y2: e.y2,
                        w: e.x2 - e.x1,
                        h: e.y2 - e.y1
                    };
                    if (null != e.w && null != e.h && e.w >= 0 && e.h >= 0) return {
                        x1: e.x1,
                        y1: e.y1,
                        x2: e.x1 + e.w,
                        y2: e.y1 + e.h,
                        w: e.w,
                        h: e.h
                    }
                }
            }, n.updateBoundingBox = function (e, t) {
                e.x1 = Math.min(e.x1, t.x1), e.x2 = Math.max(e.x2, t.x2), e.w = e.x2 - e.x1, e.y1 = Math.min(e.y1, t.y1), e.y2 = Math.max(e.y2, t.y2), e.h = e.y2 - e.y1
            }, n.expandBoundingBox = function (e, t) {
                return e.x1 -= t, e.x2 += t, e.y1 -= t, e.y2 += t, e.w = e.x2 - e.x1, e.h = e.y2 - e.y1, e
            }, n.boundingBoxesIntersect = function (e, t) {
                return e.x1 > t.x2 ? !1 : t.x1 > e.x2 ? !1 : e.x2 < t.x1 ? !1 : t.x2 < e.x1 ? !1 : e.y2 < t.y1 ? !1 : t.y2 < e.y1 ? !1 : e.y1 > t.y2 ? !1 : !(t.y1 > e.y2)
            }, n.inBoundingBox = function (e, t, r) {
                return e.x1 <= t && t <= e.x2 && e.y1 <= r && r <= e.y2
            }, n.pointInBoundingBox = function (e, t) {
                return this.inBoundingBox(e, t.x, t.y)
            }, n.boundingBoxInBoundingBox = function (e, t) {
                return n.inBoundingBox(e, t.x1, t.y1) && n.inBoundingBox(e, t.x2, t.y2)
            }, n.roundRectangleIntersectLine = function (e, t, r, n, i, a, o) {
                var s, l = this.getRoundRectangleRadius(i, a),
                    u = i / 2,
                    c = a / 2,
                    d = r - u + l - o,
                    h = n - c - o,
                    p = r + u - l + o,
                    f = h;
                if (s = this.finiteLinesIntersect(e, t, r, n, d, h, p, f, !1), s.length > 0) return s;
                var v = r + u + o,
                    g = n - c + l - o,
                    y = v,
                    m = n + c - l + o;
                if (s = this.finiteLinesIntersect(e, t, r, n, v, g, y, m, !1), s.length > 0) return s;
                var b = r - u + l - o,
                    x = n + c + o,
                    w = r + u - l + o,
                    E = x;
                if (s = this.finiteLinesIntersect(e, t, r, n, b, x, w, E, !1), s.length > 0) return s;
                var _ = r - u - o,
                    S = n - c + l - o,
                    P = _,
                    T = n + c - l + o;
                if (s = this.finiteLinesIntersect(e, t, r, n, _, S, P, T, !1), s.length > 0) return s;
                var k, D = r - u + l,
                    C = n - c + l;
                if (k = this.intersectLineCircle(e, t, r, n, D, C, l + o), k.length > 0 && k[0] <= D && k[1] <= C) return [k[0], k[1]];
                var M = r + u - l,
                    N = n - c + l;
                if (k = this.intersectLineCircle(e, t, r, n, M, N, l + o), k.length > 0 && k[0] >= M && k[1] <= N) return [k[0], k[1]];
                var B = r + u - l,
                    z = n + c - l;
                if (k = this.intersectLineCircle(e, t, r, n, B, z, l + o), k.length > 0 && k[0] >= B && k[1] >= z) return [k[0], k[1]];
                var I = r - u + l,
                    L = n + c - l;
                return k = this.intersectLineCircle(e, t, r, n, I, L, l + o), k.length > 0 && k[0] <= I && k[1] >= L ? [k[0], k[1]] : []
            }, n.inLineVicinity = function (e, t, r, n, i, a, o) {
                var s = o,
                    l = Math.min(r, i),
                    u = Math.max(r, i),
                    c = Math.min(n, a),
                    d = Math.max(n, a);
                return e >= l - s && u + s >= e && t >= c - s && d + s >= t
            }, n.inBezierVicinity = function (e, t, r, n, i, a, o, s, l) {
                var u = {
                    x1: Math.min(r, o, i) - l,
                    x2: Math.max(r, o, i) + l,
                    y1: Math.min(n, s, a) - l,
                    y2: Math.max(n, s, a) + l
                };
                return !(e < u.x1 || e > u.x2 || t < u.y1 || t > u.y2)
            }, n.solveCubic = function (e, t, r, n, i) {
                t /= e, r /= e, n /= e;
                var a, o, s, l, u, c, d, h;
                return o = (3 * r - t * t) / 9, s = -(27 * n) + t * (9 * r - 2 * (t * t)), s /= 54, a = o * o * o + s * s, i[1] = 0, d = t / 3, a > 0 ? (u = s + Math.sqrt(a), u = 0 > u ? -Math.pow(-u, 1 / 3) : Math.pow(u, 1 / 3), c = s - Math.sqrt(a), c = 0 > c ? -Math.pow(-c, 1 / 3) : Math.pow(c, 1 / 3), i[0] = -d + u + c, d += (u + c) / 2, i[4] = i[2] = -d, d = Math.sqrt(3) * (-c + u) / 2, i[3] = d, void (i[5] = -d)) : (i[5] = i[3] = 0, 0 === a ? (h = 0 > s ? -Math.pow(-s, 1 / 3) : Math.pow(s, 1 / 3), i[0] = -d + 2 * h, void (i[4] = i[2] = -(h + d))) : (o = -o, l = o * o * o, l = Math.acos(s / Math.sqrt(l)), h = 2 * Math.sqrt(o), i[0] = -d + h * Math.cos(l / 3), i[2] = -d + h * Math.cos((l + 2 * Math.PI) / 3), void (i[4] = -d + h * Math.cos((l + 4 * Math.PI) / 3))))
            }, n.sqdistToQuadraticBezier = function (e, t, r, n, i, a, o, s) {
                var l = 1 * r * r - 4 * r * i + 2 * r * o + 4 * i * i - 4 * i * o + o * o + n * n - 4 * n * a + 2 * n * s + 4 * a * a - 4 * a * s + s * s,
                    u = 9 * r * i - 3 * r * r - 3 * r * o - 6 * i * i + 3 * i * o + 9 * n * a - 3 * n * n - 3 * n * s - 6 * a * a + 3 * a * s,
                    c = 3 * r * r - 6 * r * i + r * o - r * e + 2 * i * i + 2 * i * e - o * e + 3 * n * n - 6 * n * a + n * s - n * t + 2 * a * a + 2 * a * t - s * t,
                    d = 1 * r * i - r * r + r * e - i * e + n * a - n * n + n * t - a * t,
                    h = [];
                this.solveCubic(l, u, c, d, h);
                for (var p = 1e-7, f = [], v = 0; 6 > v; v += 2) Math.abs(h[v + 1]) < p && h[v] >= 0 && h[v] <= 1 && f.push(h[v]);
                f.push(1), f.push(0);
                for (var g, y, m, b, x = -1, w = 0; w < f.length; w++) y = Math.pow(1 - f[w], 2) * r + 2 * (1 - f[w]) * f[w] * i + f[w] * f[w] * o, m = Math.pow(1 - f[w], 2) * n + 2 * (1 - f[w]) * f[w] * a + f[w] * f[w] * s, b = Math.pow(y - e, 2) + Math.pow(m - t, 2), x >= 0 ? x > b && (x = b, g = f[w]) : (x = b, g = f[w]);
                return x
            }, n.sqdistToFiniteLine = function (e, t, r, n, i, a) {
                var o = [e - r, t - n],
                    s = [i - r, a - n],
                    l = s[0] * s[0] + s[1] * s[1],
                    u = o[0] * o[0] + o[1] * o[1],
                    c = o[0] * s[0] + o[1] * s[1],
                    d = c * c / l;
                return 0 > c ? u : d > l ? (e - i) * (e - i) + (t - a) * (t - a) : u - d
            }, n.pointInsidePolygonPoints = function (e, t, r) {
                for (var n, i, a, o, s, l = 0, u = 0, c = 0; c < r.length / 2; c++)
                    if (n = r[2 * c], i = r[2 * c + 1], c + 1 < r.length / 2 ? (a = r[2 * (c + 1)], o = r[2 * (c + 1) + 1]) : (a = r[2 * (c + 1 - r.length / 2)], o = r[2 * (c + 1 - r.length / 2) + 1]), n == e && a == e);
                    else {
                        if (!(n >= e && e >= a || e >= n && a >= e)) continue;
                        s = (e - n) / (a - n) * (o - i) + i, s > t && l++ , t > s && u++
                    }
                return l % 2 !== 0
            }, n.pointInsidePolygon = function (e, t, r, i, a, o, s, l, u) {
                var c, d = new Array(r.length);
                null != l[0] ? (c = Math.atan(l[1] / l[0]), l[0] < 0 ? c += Math.PI / 2 : c = -c - Math.PI / 2) : c = l;
                for (var h = Math.cos(-c), p = Math.sin(-c), f = 0; f < d.length / 2; f++) d[2 * f] = o / 2 * (r[2 * f] * h - r[2 * f + 1] * p), d[2 * f + 1] = s / 2 * (r[2 * f + 1] * h + r[2 * f] * p), d[2 * f] += i, d[2 * f + 1] += a;
                var v;
                if (u > 0) {
                    var g = this.expandPolygon(d, -u);
                    v = this.joinLines(g)
                } else v = d;
                return n.pointInsidePolygonPoints(e, t, v)
            }, n.joinLines = function (e) {
                for (var t, r, n, i, a, o, s, l, u = new Array(e.length / 2), c = 0; c < e.length / 4; c++) {
                    t = e[4 * c], r = e[4 * c + 1], n = e[4 * c + 2], i = e[4 * c + 3], c < e.length / 4 - 1 ? (a = e[4 * (c + 1)], o = e[4 * (c + 1) + 1], s = e[4 * (c + 1) + 2], l = e[4 * (c + 1) + 3]) : (a = e[0], o = e[1], s = e[2], l = e[3]);
                    var d = this.finiteLinesIntersect(t, r, n, i, a, o, s, l, !0);
                    u[2 * c] = d[0], u[2 * c + 1] = d[1]
                }
                return u
            }, n.expandPolygon = function (e, t) {
                for (var r, n, i, a, o = new Array(2 * e.length), s = 0; s < e.length / 2; s++) {
                    r = e[2 * s], n = e[2 * s + 1], s < e.length / 2 - 1 ? (i = e[2 * (s + 1)], a = e[2 * (s + 1) + 1]) : (i = e[0], a = e[1]);
                    var l = a - n,
                        u = -(i - r),
                        c = Math.sqrt(l * l + u * u),
                        d = l / c,
                        h = u / c;
                    o[4 * s] = r + d * t, o[4 * s + 1] = n + h * t, o[4 * s + 2] = i + d * t, o[4 * s + 3] = a + h * t
                }
                return o
            }, n.intersectLineEllipse = function (e, t, r, n, i, a) {
                var o = r - e,
                    s = n - t;
                o /= i, s /= a;
                var l = Math.sqrt(o * o + s * s),
                    u = l - 1;
                if (0 > u) return [];
                var c = u / l;
                return [(r - e) * c + e, (n - t) * c + t]
            }, n.intersectLineCircle = function (e, t, r, n, i, a, o) {
                var s = [r - e, n - t],
                    l = [i, a],
                    u = [e - i, t - a],
                    c = s[0] * s[0] + s[1] * s[1],
                    d = 2 * (u[0] * s[0] + u[1] * s[1]),
                    l = u[0] * u[0] + u[1] * u[1] - o * o,
                    h = d * d - 4 * c * l;
                if (0 > h) return [];
                var p = (-d + Math.sqrt(h)) / (2 * c),
                    f = (-d - Math.sqrt(h)) / (2 * c),
                    v = Math.min(p, f),
                    g = Math.max(p, f),
                    y = [];
                if (v >= 0 && 1 >= v && y.push(v), g >= 0 && 1 >= g && y.push(g), 0 === y.length) return [];
                var m = y[0] * s[0] + e,
                    b = y[0] * s[1] + t;
                if (y.length > 1) {
                    if (y[0] == y[1]) return [m, b];
                    var x = y[1] * s[0] + e,
                        w = y[1] * s[1] + t;
                    return [m, b, x, w]
                }
                return [m, b]
            }, n.findCircleNearPoint = function (e, t, r, n, i) {
                var a = n - e,
                    o = i - t,
                    s = Math.sqrt(a * a + o * o),
                    l = a / s,
                    u = o / s;
                return [e + l * r, t + u * r]
            }, n.findMaxSqDistanceToOrigin = function (e) {
                for (var t, r = 1e-6, n = 0; n < e.length / 2; n++) t = e[2 * n] * e[2 * n] + e[2 * n + 1] * e[2 * n + 1], t > r && (r = t);
                return r
            }, n.midOfThree = function (e, t, r) {
                return e >= t && r >= e || e >= r && t >= e ? e : t >= e && r >= t || t >= r && e >= t ? t : r
            }, n.finiteLinesIntersect = function (e, t, r, n, i, a, o, s, l) {
                var u = e - i,
                    c = r - e,
                    d = o - i,
                    h = t - a,
                    p = n - t,
                    f = s - a,
                    v = d * h - f * u,
                    g = c * h - p * u,
                    y = f * c - d * p;
                if (0 !== y) {
                    var m = v / y,
                        b = g / y,
                        x = .001,
                        w = 0 - x,
                        E = 1 + x;
                    return m >= w && E >= m && b >= w && E >= b ? [e + m * c, t + m * p] : l ? [e + m * c, t + m * p] : []
                }
                return 0 === v || 0 === g ? this.midOfThree(e, r, o) === o ? [o, s] : this.midOfThree(e, r, i) === i ? [i, a] : this.midOfThree(i, o, r) === r ? [r, n] : [] : []
            }, n.polygonIntersectLine = function (e, t, r, i, a, o, s, l) {
                for (var u, c = [], d = new Array(r.length), h = 0; h < d.length / 2; h++) d[2 * h] = r[2 * h] * o + i, d[2 * h + 1] = r[2 * h + 1] * s + a;
                var p;
                if (l > 0) {
                    var f = n.expandPolygon(d, -l);
                    p = n.joinLines(f)
                } else p = d;
                for (var v, g, y, m, h = 0; h < p.length / 2; h++) v = p[2 * h], g = p[2 * h + 1], h < p.length / 2 - 1 ? (y = p[2 * (h + 1)], m = p[2 * (h + 1) + 1]) : (y = p[0], m = p[1]), u = this.finiteLinesIntersect(e, t, i, a, v, g, y, m), 0 !== u.length && c.push(u[0], u[1]);
                return c
            }, n.shortenIntersection = function (e, t, r) {
                var n = [e[0] - t[0], e[1] - t[1]],
                    i = Math.sqrt(n[0] * n[0] + n[1] * n[1]),
                    a = (i - r) / i;
                return 0 > a && (a = 1e-5), [t[0] + a * n[0], t[1] + a * n[1]]
            }, n.generateUnitNgonPointsFitToSquare = function (e, t) {
                var r = n.generateUnitNgonPoints(e, t);
                return r = n.fitPolygonToSquare(r)
            }, n.fitPolygonToSquare = function (e) {
                for (var t, r, n = e.length / 2, i = 1 / 0, a = 1 / 0, o = -(1 / 0), s = -(1 / 0), l = 0; n > l; l++) t = e[2 * l], r = e[2 * l + 1], i = Math.min(i, t), o = Math.max(o, t), a = Math.min(a, r), s = Math.max(s, r);
                for (var u = 2 / (o - i), c = 2 / (s - a), l = 0; n > l; l++) t = e[2 * l] = e[2 * l] * u, r = e[2 * l + 1] = e[2 * l + 1] * c, i = Math.min(i, t), o = Math.max(o, t), a = Math.min(a, r), s = Math.max(s, r);
                if (-1 > a)
                    for (var l = 0; n > l; l++) r = e[2 * l + 1] = e[2 * l + 1] + (-1 - a);
                return e
            }, n.generateUnitNgonPoints = function (e, t) {
                var r = 1 / e * 2 * Math.PI,
                    n = e % 2 === 0 ? Math.PI / 2 + r / 2 : Math.PI / 2;
                n += t;
                for (var i, a, o, s = new Array(2 * e), l = 0; e > l; l++) i = l * r + n, a = s[2 * l] = Math.cos(i), o = s[2 * l + 1] = Math.sin(-i);
                return s
            }, n.getRoundRectangleRadius = function (e, t) {
                return Math.min(e / 4, t / 4, 8)
            }, t.exports = n
        }, {}],
        86: [function (e, t, r) {
            /*!
            Embeddable Minimum Strictly-Compliant Promises/A+ 1.1.1 Thenable
            Copyright (c) 2013-2014 Ralf S. Engelschall (http://engelschall.com)
            Licensed under The MIT License (http://opensource.org/licenses/MIT)
            */
            "use strict";
            var n = 0,
                i = 1,
                a = 2,
                o = function (e) {
                    return this instanceof o ? (this.id = "Thenable/1.0.7", this.state = n, this.fulfillValue = void 0, this.rejectReason = void 0, this.onFulfilled = [], this.onRejected = [], this.proxy = {
                        then: this.then.bind(this)
                    }, void ("function" == typeof e && e.call(this, this.fulfill.bind(this), this.reject.bind(this)))) : new o(e)
                };
            o.prototype = {
                fulfill: function (e) {
                    return s(this, i, "fulfillValue", e)
                },
                reject: function (e) {
                    return s(this, a, "rejectReason", e)
                },
                then: function (e, t) {
                    var r = this,
                        n = new o;
                    return r.onFulfilled.push(c(e, n, "fulfill")), r.onRejected.push(c(t, n, "reject")), l(r), n.proxy
                }
            };
            var s = function (e, t, r, i) {
                return e.state === n && (e.state = t, e[r] = i, l(e)), e
            },
                l = function (e) {
                    e.state === i ? u(e, "onFulfilled", e.fulfillValue) : e.state === a && u(e, "onRejected", e.rejectReason)
                },
                u = function (e, t, r) {
                    if (0 !== e[t].length) {
                        var n = e[t];
                        e[t] = [];
                        var i = function () {
                            for (var e = 0; e < n.length; e++) n[e](r)
                        };
                        "function" == typeof setImmediate ? setImmediate(i) : setTimeout(i, 0)
                    }
                },
                c = function (e, t, r) {
                    return function (n) {
                        if ("function" != typeof e) t[r].call(t, n);
                        else {
                            var i;
                            try {
                                i = e(n)
                            } catch (a) {
                                return void t.reject(a)
                            }
                            d(t, i)
                        }
                    }
                },
                d = function (e, t) {
                    if (e === t || e.proxy === t) return void e.reject(new TypeError("cannot resolve promise with itself"));
                    var r;
                    if ("object" == typeof t && null !== t || "function" == typeof t) try {
                        r = t.then
                    } catch (n) {
                        return void e.reject(n)
                    }
                    if ("function" != typeof r) e.fulfill(t);
                    else {
                        var i = !1;
                        try {
                            r.call(t, function (r) {
                                i || (i = !0, r === t ? e.reject(new TypeError("circular thenable chain")) : d(e, r))
                            }, function (t) {
                                i || (i = !0, e.reject(t))
                            })
                        } catch (n) {
                            i || e.reject(n)
                        }
                    }
                };
            o.all = function (e) {
                return new o(function (t, r) {
                    for (var n = new Array(e.length), i = 0, a = function (r, a) {
                        n[r] = a, i++ , i === e.length && t(n)
                    }, o = 0; o < e.length; o++) ! function (t) {
                        var n = e[t],
                            i = null != n && null != n.then;
                        if (i) n.then(function (e) {
                            a(t, e)
                        }, function (e) {
                            r(e)
                        });
                        else {
                            var o = n;
                            a(t, o)
                        }
                    }(o)
                })
            }, o.resolve = function (e) {
                return new o(function (t, r) {
                    t(e)
                })
            }, o.reject = function (e) {
                return new o(function (t, r) {
                    r(e)
                })
            }, t.exports = "undefined" != typeof Promise ? Promise : o
        }, {}],
        87: [function (e, t, r) {
            "use strict";
            var n = e("./is"),
                i = e("./util"),
                a = function (e) {
                    if (!(this instanceof a)) return new a(e);
                    var t = this;
                    t._private = {
                        selectorText: null,
                        invalid: !0
                    };
                    var r = function () {
                        return {
                            classes: [],
                            colonSelectors: [],
                            data: [],
                            group: null,
                            ids: [],
                            meta: [],
                            collection: null,
                            filter: null,
                            parent: null,
                            ancestor: null,
                            subject: null,
                            child: null,
                            descendant: null
                        }
                    };
                    if (!e || n.string(e) && e.match(/^\s*$/)) t.length = 0;
                    else if ("*" === e || "edge" === e || "node" === e) t[0] = r(), t[0].group = "*" === e ? e : e + "s", t[0].groupOnly = !0, t._private.invalid = !1, t._private.selectorText = e, t.length = 1;
                    else if (n.elementOrCollection(e)) {
                        var o = e.collection();
                        t[0] = r(), t[0].collection = o, t.length = 1
                    } else if (n.fn(e)) t[0] = r(), t[0].filter = e, t.length = 1;
                    else {
                        if (!n.string(e)) return void i.error("A selector must be created from a string; found " + e);
                        var s = null,
                            l = {
                                metaChar: "[\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\]\\^\\`\\{\\|\\}\\~]",
                                comparatorOp: "=|\\!=|>|>=|<|<=|\\$=|\\^=|\\*=",
                                boolOp: "\\?|\\!|\\^",
                                string: '"(?:\\\\"|[^"])*"|' + "'(?:\\\\'|[^'])*'",
                                number: i.regex.number,
                                meta: "degree|indegree|outdegree",
                                separator: "\\s*,\\s*",
                                descendant: "\\s+",
                                child: "\\s+>\\s+",
                                subject: "\\$"
                            };
                        l.variable = "(?:[\\w-]|(?:\\\\" + l.metaChar + "))+", l.value = l.string + "|" + l.number, l.className = l.variable, l.id = l.variable;
                        for (var u = function (e) {
                            return e.replace(new RegExp("\\\\(" + l.metaChar + ")", "g"), function (e, t, r, n) {
                                return t
                            })
                        }, c = l.comparatorOp.split("|"), d = 0; d < c.length; d++) {
                            var h = c[d];
                            l.comparatorOp += "|@" + h
                        }
                        for (var c = l.comparatorOp.split("|"), d = 0; d < c.length; d++) {
                            var h = c[d];
                            h.indexOf("!") >= 0 || "=" !== h && (l.comparatorOp += "|\\!" + h)
                        }
                        var p = [{
                            name: "group",
                            query: !0,
                            regex: "(node|edge|\\*)",
                            populate: function (e) {
                                this.group = "*" === e ? e : e + "s"
                            }
                        }, {
                            name: "state",
                            query: !0,
                            regex: "(:selected|:unselected|:locked|:unlocked|:visible|:hidden|:transparent|:grabbed|:free|:removed|:inside|:grabbable|:ungrabbable|:animated|:unanimated|:selectable|:unselectable|:orphan|:nonorphan|:parent|:child|:loop|:simple|:active|:inactive|:touch|:backgrounding|:nonbackgrounding)",
                            populate: function (e) {
                                this.colonSelectors.push(e)
                            }
                        }, {
                            name: "id",
                            query: !0,
                            regex: "\\#(" + l.id + ")",
                            populate: function (e) {
                                this.ids.push(u(e))
                            }
                        }, {
                            name: "className",
                            query: !0,
                            regex: "\\.(" + l.className + ")",
                            populate: function (e) {
                                this.classes.push(u(e))
                            }
                        }, {
                            name: "dataExists",
                            query: !0,
                            regex: "\\[\\s*(" + l.variable + ")\\s*\\]",
                            populate: function (e) {
                                this.data.push({
                                    field: u(e)
                                })
                            }
                        }, {
                            name: "dataCompare",
                            query: !0,
                            regex: "\\[\\s*(" + l.variable + ")\\s*(" + l.comparatorOp + ")\\s*(" + l.value + ")\\s*\\]",
                            populate: function (e, t, r) {
                                var n = null != new RegExp("^" + l.string + "$").exec(r);
                                r = n ? r.substring(1, r.length - 1) : parseFloat(r), this.data.push({
                                    field: u(e),
                                    operator: t,
                                    value: r
                                })
                            }
                        }, {
                            name: "dataBool",
                            query: !0,
                            regex: "\\[\\s*(" + l.boolOp + ")\\s*(" + l.variable + ")\\s*\\]",
                            populate: function (e, t) {
                                this.data.push({
                                    field: u(t),
                                    operator: e
                                })
                            }
                        }, {
                            name: "metaCompare",
                            query: !0,
                            regex: "\\[\\[\\s*(" + l.meta + ")\\s*(" + l.comparatorOp + ")\\s*(" + l.number + ")\\s*\\]\\]",
                            populate: function (e, t, r) {
                                this.meta.push({
                                    field: u(e),
                                    operator: t,
                                    value: parseFloat(r)
                                })
                            }
                        }, {
                            name: "nextQuery",
                            separator: !0,
                            regex: l.separator,
                            populate: function () {
                                t[++d] = r(), s = null
                            }
                        }, {
                            name: "child",
                            separator: !0,
                            regex: l.child,
                            populate: function () {
                                var e = r();
                                e.parent = this, e.subject = s, t[d] = e
                            }
                        }, {
                            name: "descendant",
                            separator: !0,
                            regex: l.descendant,
                            populate: function () {
                                var e = r();
                                e.ancestor = this, e.subject = s, t[d] = e
                            }
                        }, {
                            name: "subject",
                            modifier: !0,
                            regex: l.subject,
                            populate: function () {
                                return null != s && this.subject != this ? (i.error("Redefinition of subject in selector `" + e + "`"), !1) : (s = this, void (this.subject = this))
                            }
                        }];
                        t._private.selectorText = e;
                        var f = e,
                            d = 0,
                            v = function (e) {
                                for (var t, r, i, a = 0; a < p.length; a++) {
                                    var o = p[a],
                                        s = o.name;
                                    if (!n.fn(e) || e(s, o)) {
                                        var l = f.match(new RegExp("^" + o.regex));
                                        if (null != l) {
                                            r = l, t = o, i = s;
                                            var u = l[0];
                                            f = f.substring(u.length);
                                            break
                                        }
                                    }
                                }
                                return {
                                    expr: t,
                                    match: r,
                                    name: i
                                }
                            },
                            g = function () {
                                var e = f.match(/^\s+/);
                                if (e) {
                                    var t = e[0];
                                    f = f.substring(t.length)
                                }
                            };
                        for (t[0] = r(), g(); ;) {
                            var y = v();
                            if (null == y.expr) return void i.error("The selector `" + e + "`is invalid");
                            for (var m = [], b = 1; b < y.match.length; b++) m.push(y.match[b]);
                            var x = y.expr.populate.apply(t[d], m);
                            if (x === !1) return;
                            if (f.match(/^\s*$/)) break
                        }
                        t.length = d + 1;
                        for (var b = 0; b < t.length; b++) {
                            var w = t[b];
                            if (null != w.subject) {
                                for (; w.subject != w;)
                                    if (null != w.parent) {
                                        var E = w.parent,
                                            _ = w;
                                        _.parent = null, E.child = _, w = E
                                    } else {
                                        if (null == w.ancestor) {
                                            i.error("When adjusting references for the selector `" + w + "`, neither parent nor ancestor was found");
                                            break
                                        }
                                        var S = w.ancestor,
                                            P = w;
                                        P.ancestor = null, S.descendant = P, w = S
                                    }
                                t[b] = w.subject
                            }
                        }
                    }
                    t._private.invalid = !1
                },
                o = a.prototype;
            o.size = function () {
                return this.length
            }, o.eq = function (e) {
                return this[e]
            };
            var s = function (e, t) {
                var r = t._private;
                if (e.groupOnly) return "*" === e.group || e.group === r.group;
                if (null != e.group && "*" != e.group && e.group != r.group) return !1;
                for (var i = t.cy(), a = !0, o = 0; o < e.colonSelectors.length; o++) {
                    var l = e.colonSelectors[o];
                    switch (l) {
                        case ":selected":
                            a = t.selected();
                            break;
                        case ":unselected":
                            a = !t.selected();
                            break;
                        case ":selectable":
                            a = t.selectable();
                            break;
                        case ":unselectable":
                            a = !t.selectable();
                            break;
                        case ":locked":
                            a = t.locked();
                            break;
                        case ":unlocked":
                            a = !t.locked();
                            break;
                        case ":visible":
                            a = t.visible();
                            break;
                        case ":hidden":
                            a = !t.visible();
                            break;
                        case ":transparent":
                            a = t.transparent();
                            break;
                        case ":grabbed":
                            a = t.grabbed();
                            break;
                        case ":free":
                            a = !t.grabbed();
                            break;
                        case ":removed":
                            a = t.removed();
                            break;
                        case ":inside":
                            a = !t.removed();
                            break;
                        case ":grabbable":
                            a = t.grabbable();
                            break;
                        case ":ungrabbable":
                            a = !t.grabbable();
                            break;
                        case ":animated":
                            a = t.animated();
                            break;
                        case ":unanimated":
                            a = !t.animated();
                            break;
                        case ":parent":
                            a = t.isNode() && t.children().nonempty();
                            break;
                        case ":child":
                        case ":nonorphan":
                            a = t.isNode() && t.parent().nonempty();
                            break;
                        case ":orphan":
                            a = t.isNode() && t.parent().empty();
                            break;
                        case ":loop":
                            a = t.isEdge() && t.data("source") === t.data("target");
                            break;
                        case ":simple":
                            a = t.isEdge() && t.data("source") !== t.data("target");
                            break;
                        case ":active":
                            a = t.active();
                            break;
                        case ":inactive":
                            a = !t.active();
                            break;
                        case ":touch":
                            a = n.touch();
                            break;
                        case ":backgrounding":
                            a = t.backgrounding();
                            break;
                        case ":nonbackgrounding":
                            a = !t.backgrounding()
                    }
                    if (!a) break
                }
                if (!a) return !1;
                for (var u = !0, o = 0; o < e.ids.length; o++) {
                    var c = e.ids[o],
                        d = r.data.id;
                    if (u = u && c == d, !u) break
                }
                if (!u) return !1;
                for (var h = !0, o = 0; o < e.classes.length; o++) {
                    var p = e.classes[o];
                    if (h = h && t.hasClass(p), !h) break
                }
                if (!h) return !1;
                var f = function (t) {
                    for (var r = !0, i = 0; i < e[t.name].length; i++) {
                        var a, o = e[t.name][i],
                            s = o.operator,
                            l = o.value,
                            u = o.field;
                        if (null != s && null != l) {
                            var c = t.fieldValue(u),
                                d = n.string(c) || n.number(c) ? "" + c : "",
                                h = "" + l,
                                p = !1;
                            s.indexOf("@") >= 0 && (d = d.toLowerCase(), h = h.toLowerCase(), s = s.replace("@", ""), p = !0);
                            var f = !1;
                            s.indexOf("!") >= 0 && (s = s.replace("!", ""), f = !0), p && (l = h.toLowerCase(), c = d.toLowerCase());
                            var v = !1;
                            switch (s) {
                                case "*=":
                                    a = d.indexOf(h) >= 0;
                                    break;
                                case "$=":
                                    a = d.indexOf(h, d.length - h.length) >= 0;
                                    break;
                                case "^=":
                                    a = 0 === d.indexOf(h);
                                    break;
                                case "=":
                                    a = c === l;
                                    break;
                                case ">":
                                    v = !0, a = c > l;
                                    break;
                                case ">=":
                                    v = !0, a = c >= l;
                                    break;
                                case "<":
                                    v = !0, a = l > c;
                                    break;
                                case "<=":
                                    v = !0, a = l >= c;
                                    break;
                                default:
                                    a = !1
                            }!f || null == c && v || (a = !a)
                        } else if (null != s) switch (s) {
                            case "?":
                                a = t.fieldTruthy(u);
                                break;
                            case "!":
                                a = !t.fieldTruthy(u);
                                break;
                            case "^":
                                a = t.fieldUndefined(u)
                        } else a = !t.fieldUndefined(u);
                        if (!a) {
                            r = !1;
                            break
                        }
                    }
                    return r
                },
                    v = f({
                        name: "data",
                        fieldValue: function (e) {
                            return r.data[e]
                        },
                        fieldUndefined: function (e) {
                            return void 0 === r.data[e]
                        },
                        fieldTruthy: function (e) {
                            return !!r.data[e]
                        }
                    });
                if (!v) return !1;
                var g = f({
                    name: "meta",
                    fieldValue: function (e) {
                        return t[e]()
                    },
                    fieldUndefined: function (e) {
                        return null == t[e]()
                    },
                    fieldTruthy: function (e) {
                        return !!t[e]()
                    }
                });
                if (!g) return !1;
                if (null != e.collection) {
                    var y = e.collection.hasElementWithId(t.id());
                    if (!y) return !1
                }
                if (null != e.filter && 0 === t.collection().filter(e.filter).size()) return !1;
                var m = function (e, t) {
                    if (null != e) {
                        var r = !1;
                        if (!i.hasCompoundNodes()) return !1;
                        t = t();
                        for (var n = 0; n < t.length; n++)
                            if (s(e, t[n])) {
                                r = !0;
                                break
                            }
                        return r
                    }
                    return !0
                };
                return m(e.parent, function () {
                    return t.parent()
                }) && m(e.ancestor, function () {
                    return t.parents()
                }) && m(e.child, function () {
                    return t.children()
                }) ? !!m(e.descendant, function () {
                    return t.descendants()
                }) : !1
            };
            o.filter = function (e) {
                var t = this,
                    r = e.cy();
                if (t._private.invalid) return r.collection();
                var n = function (e, r) {
                    for (var n = 0; n < t.length; n++) {
                        var i = t[n];
                        if (s(i, r)) return !0
                    }
                    return !1
                };
                null == t._private.selectorText && (n = function () {
                    return !0
                });
                var i = e.filter(n);
                return i
            }, o.matches = function (e) {
                var t = this;
                if (t._private.invalid) return !1;
                for (var r = 0; r < t.length; r++) {
                    var n = t[r];
                    if (s(n, e)) return !0
                }
                return !1
            }, o.toString = o.selector = function () {
                for (var e = "", t = function (e) {
                    return null == e ? "" : e
                }, r = function (e) {
                    return n.string(e) ? '"' + e + '"' : t(e)
                }, i = function (e) {
                    return " " + e + " "
                }, a = function (e) {
                    var n = "";
                    e.subject === e && (n += "$");
                    var s = t(e.group);
                    n += s.substring(0, s.length - 1);
                    for (var l = 0; l < e.data.length; l++) {
                        var u = e.data[l];
                        n += u.value ? "[" + u.field + i(t(u.operator)) + r(u.value) + "]" : "[" + t(u.operator) + u.field + "]"
                    }
                    for (var l = 0; l < e.meta.length; l++) {
                        var c = e.meta[l];
                        n += "[[" + c.field + i(t(c.operator)) + r(c.value) + "]]"
                    }
                    for (var l = 0; l < e.colonSelectors.length; l++) {
                        var d = e.colonSelectors[o];
                        n += d
                    }
                    for (var l = 0; l < e.ids.length; l++) {
                        var d = "#" + e.ids[o];
                        n += d
                    }
                    for (var l = 0; l < e.classes.length; l++) {
                        var d = "." + e.classes[l];
                        n += d
                    }
                    return null != e.parent && (n = a(e.parent) + " > " + n), null != e.ancestor && (n = a(e.ancestor) + " " + n), null != e.child && (n += " > " + a(e.child)), null != e.descendant && (n += " " + a(e.descendant)), n
                }, o = 0; o < this.length; o++) {
                    var s = this[o];
                    e += a(s), this.length > 1 && o < this.length - 1 && (e += ", ")
                }
                return e
            }, t.exports = a
        }, {
            "./is": 83,
            "./util": 100
        }],
        88: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = {};
            a.apply = function (e) {
                var t = this,
                    r = t._private;
                r.newStyle && (r.contextStyles = {}, r.propDiffs = {}, t.cleanElements(e, !0));
                for (var n = 0; n < e.length; n++) {
                    var i = e[n],
                        a = t.getContextMeta(i),
                        o = t.getContextStyle(a),
                        s = t.applyContextStyle(a, o, i);
                    t.updateTransitions(i, s.diffProps), t.updateStyleHints(i)
                }
                r.newStyle = !1
            }, a.getPropertiesDiff = function (e, t) {
                var r = this,
                    n = r._private.propDiffs = r._private.propDiffs || {},
                    i = e + "-" + t,
                    a = n[i];
                if (a) return a;
                for (var o = [], s = {}, l = 0; l < r.length; l++) {
                    var u = r[l],
                        c = "t" === e[l],
                        d = "t" === t[l],
                        h = c !== d,
                        p = u.mappedProperties.length > 0;
                    if (h || p) {
                        var f;
                        h && p ? f = u.properties : h ? f = u.properties : p && (f = u.mappedProperties);
                        for (var v = 0; v < f.length; v++) {
                            for (var g = f[v], y = g.name, m = !1, b = l + 1; b < r.length; b++) {
                                var x = r[b],
                                    w = "t" === t[b];
                                if (w && (m = null != x.properties[g.name])) break
                            }
                            s[y] || m || (s[y] = !0, o.push(y))
                        }
                    }
                }
                return n[i] = o, o
            }, a.getContextMeta = function (e) {
                var t, r = this,
                    n = "",
                    i = e._private.styleCxtKey || "";
                r._private.newStyle && (i = "");
                for (var a = 0; a < r.length; a++) {
                    var o = r[a],
                        s = o.selector && o.selector.matches(e);
                    n += s ? "t" : "f"
                }
                return t = r.getPropertiesDiff(i, n), e._private.styleCxtKey = n, {
                    key: n,
                    diffPropNames: t
                }
            }, a.getContextStyle = function (e) {
                var t = e.key,
                    r = this,
                    n = this._private.contextStyles = this._private.contextStyles || {};
                if (n[t]) return n[t];
                for (var i = {
                    _private: {
                        key: t
                    }
                }, a = 0; a < r.length; a++) {
                    var o = r[a],
                        s = "t" === t[a];
                    if (s)
                        for (var l = 0; l < o.properties.length; l++) {
                            var u = o.properties[l],
                                c = i[u.name] = u;
                            c.context = o
                        }
                }
                return n[t] = i, i
            }, a.applyContextStyle = function (e, t, r) {
                for (var n = this, i = e.diffPropNames, a = {}, o = 0; o < i.length; o++) {
                    var s = i[o],
                        l = t[s],
                        u = r.pstyle(s);
                    if (!l) {
                        if (!u) continue;
                        l = u.bypass ? {
                            name: s,
                            deleteBypassed: !0
                        } : {
                                name: s,
                                "delete": !0
                            }
                    }
                    if (u !== l) {
                        var c = a[s] = {
                            prev: u
                        };
                        n.applyParsedProperty(r, l), c.next = r.pstyle(s), c.next && c.next.bypass && (c.next = c.next.bypassed)
                    }
                }
                return {
                    diffProps: a
                }
            }, a.updateStyleHints = function (e) {
                var t = e._private,
                    r = this;
                if (!e.removed()) {
                    var n = !1;
                    if ("nodes" === t.group)
                        for (var i = 1; i <= r.pieBackgroundN; i++) {
                            var a = e.pstyle("pie-" + i + "-background-size").value;
                            if (a > 0) {
                                n = !0;
                                break
                            }
                        }
                    t.hasPie = n;
                    var o = e.pstyle("text-transform").strValue,
                        s = e.pstyle("label").strValue,
                        l = e.pstyle("source-label").strValue,
                        u = e.pstyle("target-label").strValue,
                        c = e.pstyle("font-style").strValue,
                        a = e.pstyle("font-size").pfValue + "px",
                        d = e.pstyle("font-family").strValue,
                        h = e.pstyle("font-weight").strValue,
                        p = e.pstyle("text-valign").strValue,
                        f = e.pstyle("text-valign").strValue,
                        v = e.pstyle("text-outline-width").pfValue,
                        g = e.pstyle("text-wrap").strValue,
                        y = e.pstyle("text-max-width").pfValue,
                        m = c + "$" + a + "$" + d + "$" + h + "$" + o + "$" + p + "$" + f + "$" + v + "$" + g + "$" + y;
                    t.labelStyleKey = m, t.sourceLabelKey = m + "$" + l, t.targetLabelKey = m + "$" + u, t.labelKey = m + "$" + s, t.fontKey = c + "$" + h + "$" + a + "$" + d, t.styleKey = Date.now()
                }
            }, a.applyParsedProperty = function (e, t) {
                var r, a, o = this,
                    s = t,
                    l = e._private.style,
                    u = o.types,
                    c = o.properties[s.name].type,
                    d = s.bypass,
                    h = l[s.name],
                    p = h && h.bypass,
                    f = e._private;
                if ("curve-style" === t.name && "haystack" === t.value && e.isEdge() && (e.isLoop() || e.source().isParent() || e.target().isParent()) && (s = t = this.parse(t.name, "bezier", d)), s["delete"]) return l[s.name] = void 0, !0;
                if (s.deleteBypassed) return h ? h.bypass ? (h.bypassed = void 0, !0) : !1 : !0;
                if (s.deleteBypass) return h ? h.bypass ? (l[s.name] = h.bypassed, !0) : !1 : !0;
                var v = function () {
                    n.error("Do not assign mappings to elements without corresponding data (e.g. ele `" + e.id() + "` for property `" + s.name + "` with data field `" + s.field + "`); try a `[" + s.field + "]` selector to limit scope to elements with `" + s.field + "` defined")
                };
                switch (s.mapped) {
                    case u.mapData:
                    case u.mapLayoutData:
                    case u.mapScratch:
                        var r, g = s.mapped === u.mapLayoutData,
                            y = s.mapped === u.mapScratch,
                            m = s.field.split(".");
                        r = y || g ? f.scratch : f.data;
                        for (var b = 0; b < m.length && r; b++) {
                            var x = m[b];
                            r = r[x]
                        }
                        var w;
                        if (w = i.number(r) ? (r - s.fieldMin) / (s.fieldMax - s.fieldMin) : 0, 0 > w ? w = 0 : w > 1 && (w = 1), c.color) {
                            var E = s.valueMin[0],
                                _ = s.valueMax[0],
                                S = s.valueMin[1],
                                P = s.valueMax[1],
                                T = s.valueMin[2],
                                k = s.valueMax[2],
                                D = null == s.valueMin[3] ? 1 : s.valueMin[3],
                                C = null == s.valueMax[3] ? 1 : s.valueMax[3],
                                M = [Math.round(E + (_ - E) * w), Math.round(S + (P - S) * w), Math.round(T + (k - T) * w), Math.round(D + (C - D) * w)];
                            a = {
                                bypass: s.bypass,
                                name: s.name,
                                value: M,
                                strValue: "rgb(" + M[0] + ", " + M[1] + ", " + M[2] + ")"
                            }
                        } else {
                            if (!c.number) return !1;
                            var N = s.valueMin + (s.valueMax - s.valueMin) * w;
                            a = this.parse(s.name, N, s.bypass, !0)
                        }
                        a || (a = this.parse(s.name, h.strValue, s.bypass, !0)), a || v(), a.mapping = s, s = a;
                        break;
                    case u.data:
                    case u.layoutData:
                    case u.scratch:
                        var r, g = s.mapped === u.layoutData,
                            y = s.mapped === u.scratch,
                            m = s.field.split(".");
                        if (r = y || g ? f.scratch : f.data)
                            for (var b = 0; b < m.length; b++) {
                                var x = m[b];
                                r = r[x]
                            }
                        if (a = this.parse(s.name, r, s.bypass, !0), !a) {
                            var B = h ? h.strValue : "";
                            a = this.parse(s.name, B, s.bypass, !0)
                        }
                        a || v(), a.mapping = s, s = a;
                        break;
                    case u.fn:
                        var z = s.value,
                            I = z(e);
                        a = this.parse(s.name, I, s.bypass, !0), a.mapping = s, s = a;
                        break;
                    case void 0:
                        break;
                    default:
                        return !1
                }
                return d ? (p ? s.bypassed = h.bypassed : s.bypassed = h, l[s.name] = s) : p ? h.bypassed = s : l[s.name] = s, !0
            }, a.cleanElements = function (e, t) {
                for (var r = this, n = r.properties, i = 0; i < e.length; i++) {
                    var a = e[i];
                    if (t)
                        for (var o = a._private.style, s = 0; s < n.length; s++) {
                            var l = n[s],
                                u = o[l.name];
                            u && (u.bypass ? u.bypassed = null : o[l.name] = null)
                        } else a._private.style = {}
                }
            }, a.update = function () {
                var e = this._private.cy,
                    t = e.mutableElements();
                t.updateStyle()
            }, a.updateMappers = function (e) {
                for (var t = this, r = 0; r < e.length; r++) {
                    for (var n = e[r], i = n._private.style, a = 0; a < t.properties.length; a++) {
                        var o = t.properties[a],
                            s = i[o.name];
                        if (s && s.mapping) {
                            var l = s.mapping;
                            this.applyParsedProperty(n, l)
                        }
                    }
                    this.updateStyleHints(n)
                }
            }, a.updateTransitions = function (e, t, r) {
                var n = this,
                    a = e._private,
                    o = e.pstyle("transition-property").value,
                    s = e.pstyle("transition-duration").pfValue,
                    l = e.pstyle("transition-delay").pfValue;
                if (o.length > 0 && s > 0) {
                    for (var u = {}, c = !1, d = 0; d < o.length; d++) {
                        var h = o[d],
                            p = e.pstyle(h),
                            f = t[h];
                        if (f) {
                            var v, g = f.prev,
                                y = g,
                                m = null != f.next ? f.next : p,
                                b = !1,
                                x = 1e-6;
                            y && (i.number(y.pfValue) && i.number(m.pfValue) ? (b = m.pfValue - y.pfValue, v = y.pfValue + x * b) : i.number(y.value) && i.number(m.value) ? (b = m.value - y.value, v = y.value + x * b) : i.array(y.value) && i.array(m.value) && (b = y.value[0] !== m.value[0] || y.value[1] !== m.value[1] || y.value[2] !== m.value[2], v = y.strValue), b && (u[h] = m.strValue, this.applyBypass(e, h, v), c = !0))
                        }
                    }
                    if (!c) return;
                    a.transitioning = !0, e.stop(), l > 0 && e.delay(l), e.animate({
                        css: u
                    }, {
                            duration: s,
                            easing: e.pstyle("transition-timing-function").value,
                            queue: !1,
                            complete: function () {
                                r || n.removeBypasses(e, o), a.transitioning = !1
                            }
                        })
                } else a.transitioning && (e.stop(), this.removeBypasses(e, o), a.transitioning = !1)
            }, t.exports = a
        }, {
            "../is": 83,
            "../util": 100
        }],
        89: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../util"),
                a = {};
            a.applyBypass = function (e, t, r, a) {
                var o = this,
                    s = [],
                    l = !0;
                if ("*" === t || "**" === t) {
                    if (void 0 !== r)
                        for (var u = 0; u < o.properties.length; u++) {
                            var c = o.properties[u],
                                t = c.name,
                                d = this.parse(t, r, !0);
                            d && s.push(d)
                        }
                } else if (n.string(t)) {
                    var d = this.parse(t, r, !0);
                    d && s.push(d)
                } else {
                    if (!n.plainObject(t)) return !1;
                    var h = t;
                    a = r;
                    for (var u = 0; u < o.properties.length; u++) {
                        var c = o.properties[u],
                            t = c.name,
                            r = h[t];
                        if (void 0 === r && (r = h[i.dash2camel(t)]), void 0 !== r) {
                            var d = this.parse(t, r, !0);
                            d && s.push(d)
                        }
                    }
                }
                if (0 === s.length) return !1;
                for (var p = !1, u = 0; u < e.length; u++) {
                    for (var f, v = e[u], g = {}, y = 0; y < s.length; y++) {
                        var c = s[y];
                        if (a) {
                            var m = v.pstyle(c.name);
                            f = g[c.name] = {
                                prev: m
                            }
                        }
                        p = this.applyParsedProperty(v, c) || p, a && (f.next = v.pstyle(c.name))
                    }
                    p && this.updateStyleHints(v), a && this.updateTransitions(v, g, l)
                }
                return p
            }, a.overrideBypass = function (e, t, r) {
                t = i.camel2dash(t);
                for (var n = 0; n < e.length; n++) {
                    var a = e[n],
                        o = a._private.style[t],
                        s = this.properties[t].type,
                        l = s.color,
                        u = s.mutiple;
                    o && o.bypass ? (o.value = r, null != o.pfValue && (o.pfValue = r), l ? o.strValue = "rgb(" + r.join(",") + ")" : u ? o.strValue = r.join(" ") : o.strValue = "" + r) : this.applyBypass(a, t, r)
                }
            }, a.removeAllBypasses = function (e, t) {
                return this.removeBypasses(e, this.propertyNames, t)
            }, a.removeBypasses = function (e, t, r) {
                for (var n = !0, i = 0; i < e.length; i++) {
                    for (var a = e[i], o = {}, s = 0; s < t.length; s++) {
                        var l = t[s],
                            u = this.properties[l],
                            c = a.pstyle(u.name);
                        if (c && c.bypass) {
                            var d = "",
                                h = this.parse(l, d, !0),
                                p = o[u.name] = {
                                    prev: c
                                };
                            this.applyParsedProperty(a, h), p.next = a.pstyle(u.name)
                        }
                    }
                    this.updateStyleHints(a), r && this.updateTransitions(a, o, n)
                }
            }, t.exports = a
        }, {
            "../is": 83,
            "../util": 100
        }],
        90: [function (e, t, r) {
            "use strict";
            var n = e("../window"),
                i = {};
            i.getEmSizeInPixels = function () {
                var e = this.containerCss("font-size");
                return null != e ? parseFloat(e) : 1
            }, i.containerCss = function (e) {
                var t = this._private.cy,
                    r = t.container();
                return n && r && n.getComputedStyle ? n.getComputedStyle(r).getPropertyValue(e) : void 0
            }, t.exports = i
        }, {
            "../window": 107
        }],
        91: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = {};
            a.getRenderedStyle = function (e) {
                return this.getRawStyle(e, !0)
            }, a.getRawStyle = function (e, t) {
                var r = this,
                    e = e[0];
                if (e) {
                    for (var i = {}, a = 0; a < r.properties.length; a++) {
                        var o = r.properties[a],
                            s = r.getStylePropertyValue(e, o.name, t);
                        s && (i[o.name] = s, i[n.dash2camel(o.name)] = s)
                    }
                    return i
                }
            }, a.getStylePropertyValue = function (e, t, r) {
                var n = this,
                    e = e[0];
                if (e) {
                    var i = n.properties[t];
                    i.alias && (i = i.pointsTo);
                    var a = i.type,
                        o = e.pstyle(i.name),
                        s = e.cy().zoom();
                    if (o) {
                        var l = o.units ? a.implicitUnits || "px" : null,
                            u = l ? [].concat(o.pfValue).map(function (e) {
                                return e * (r ? s : 1) + l
                            }).join(" ") : o.strValue;
                        return u
                    }
                }
            }, a.getAnimationStartStyle = function (e, t) {
                for (var r = {}, n = 0; n < t.length; n++) {
                    var a = t[n],
                        o = a.name,
                        s = e.pstyle(o);
                    void 0 !== s && (s = i.plainObject(s) ? this.parse(o, s.strValue) : this.parse(o, s)), s && (r[o] = s)
                }
                return r
            }, a.getPropsList = function (e) {
                var t = this,
                    r = [],
                    i = e,
                    a = t.properties;
                if (i)
                    for (var o = Object.keys(i), s = 0; s < o.length; s++) {
                        var l = o[s],
                            u = i[l],
                            c = a[l] || a[n.camel2dash(l)],
                            d = this.parse(c.name, u);
                        r.push(d)
                    }
                return r
            }, t.exports = a
        }, {
            "../is": 83,
            "../util": 100
        }],
        92: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../util"),
                a = e("../selector"),
                o = function (e) {
                    return this instanceof o ? n.core(e) ? (this._private = {
                        cy: e,
                        coreStyle: {}
                    }, this.length = 0, void this.resetToDefault()) : void i.error("A style must have a core reference") : new o(e)
                },
                s = o.prototype;
            s.instanceString = function () {
                return "style"
            }, s.clear = function () {
                for (var e = 0; e < this.length; e++) this[e] = void 0;
                this.length = 0;
                var t = this._private;
                return t.newStyle = !0, this
            }, s.resetToDefault = function () {
                return this.clear(), this.addDefaultStylesheet(), this
            }, s.core = function () {
                return this._private.coreStyle
            }, s.selector = function (e) {
                var t = "core" === e ? null : new a(e),
                    r = this.length++;
                return this[r] = {
                    selector: t,
                    properties: [],
                    mappedProperties: [],
                    index: r
                }, this
            }, s.css = function () {
                var e = this,
                    t = arguments;
                switch (t.length) {
                    case 1:
                        for (var r = t[0], n = 0; n < e.properties.length; n++) {
                            var a = e.properties[n],
                                o = r[a.name];
                            void 0 === o && (o = r[i.dash2camel(a.name)]), void 0 !== o && this.cssRule(a.name, o)
                        }
                        break;
                    case 2:
                        this.cssRule(t[0], t[1])
                }
                return this
            }, s.style = s.css, s.cssRule = function (e, t) {
                var r = this.parse(e, t);
                if (r) {
                    var n = this.length - 1;
                    this[n].properties.push(r), this[n].properties[r.name] = r, r.name.match(/pie-(\d+)-background-size/) && r.value && (this._private.hasPie = !0), r.mapped && this[n].mappedProperties.push(r);
                    var i = !this[n].selector;
                    i && (this._private.coreStyle[r.name] = r)
                }
                return this
            }, o.fromJson = function (e, t) {
                var r = new o(e);
                return r.fromJson(t), r
            }, o.fromString = function (e, t) {
                return new o(e).fromString(t)
            }, [e("./apply"), e("./bypass"), e("./container"), e("./get-for-ele"), e("./json"), e("./string-sheet"), e("./properties"), e("./parse")].forEach(function (e) {
                i.extend(s, e)
            }), o.types = s.types, o.properties = s.properties, t.exports = o
        }, {
            "../is": 83,
            "../selector": 87,
            "../util": 100,
            "./apply": 88,
            "./bypass": 89,
            "./container": 90,
            "./get-for-ele": 91,
            "./json": 93,
            "./parse": 94,
            "./properties": 95,
            "./string-sheet": 96
        }],
        93: [function (e, t, r) {
            "use strict";
            var n = {};
            n.applyFromJson = function (e) {
                for (var t = this, r = 0; r < e.length; r++) {
                    var n = e[r],
                        i = n.selector,
                        a = n.style || n.css,
                        o = Object.keys(a);
                    t.selector(i);
                    for (var s = 0; s < o.length; s++) {
                        var l = o[s],
                            u = a[l];
                        t.css(l, u)
                    }
                }
                return t
            }, n.fromJson = function (e) {
                var t = this;
                return t.resetToDefault(), t.applyFromJson(e), t
            }, n.json = function () {
                for (var e = [], t = this.defaultLength; t < this.length; t++) {
                    for (var r = this[t], n = r.selector, i = r.properties, a = {}, o = 0; o < i.length; o++) {
                        var s = i[o];
                        a[s.name] = s.strValue
                    }
                    e.push({
                        selector: n ? n.toString() : "core",
                        style: a
                    })
                }
                return e
            }, t.exports = n
        }, {}],
        94: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../is"),
                a = e("../math"),
                o = {};
            o.parse = function (e, t, r, a) {
                var o = this;
                if (i.fn(t)) return o.parseImpl(e, t, r, a);
                var s, l = [e, t, r, a].join("$"),
                    u = o.propCache = o.propCache || {};
                return (s = u[l]) || (s = u[l] = o.parseImpl(e, t, r, a)), s = n.copy(s), s && (s.value = n.copy(s.value)), s
            };
            var s = function (e, t, r, o) {
                var s = this;
                e = n.camel2dash(e);
                var l = s.properties[e],
                    u = t,
                    c = s.types;
                if (!l) return null;
                if (void 0 === t || null === t) return null;
                l.alias && (l = l.pointsTo, e = l.name);
                var d = i.string(t);
                d && (t = t.trim());
                var h = l.type;
                if (!h) return null;
                if (r && ("" === t || null === t)) return {
                    name: e,
                    value: t,
                    bypass: !0,
                    deleteBypass: !0
                };
                if (i.fn(t)) return {
                    name: e,
                    value: t,
                    strValue: "fn",
                    mapped: c.fn,
                    bypass: r
                };
                var p, f, v, g, y, m;
                if (!d || o);
                else {
                    if ((p = new RegExp(c.data.regex).exec(t)) || (v = new RegExp(c.layoutData.regex).exec(t)) || (y = new RegExp(c.scratch.regex).exec(t))) {
                        if (r) return !1;
                        var b;
                        return b = p ? c.data : v ? c.layoutData : c.scratch, p = p || v || y, {
                            name: e,
                            value: p,
                            strValue: "" + t,
                            mapped: b,
                            field: p[1],
                            bypass: r
                        }
                    }
                    if ((f = new RegExp(c.mapData.regex).exec(t)) || (g = new RegExp(c.mapLayoutData.regex).exec(t)) || (m = new RegExp(c.mapScratch.regex).exec(t))) {
                        if (r) return !1;
                        if (h.multiple) return !1;
                        var b;
                        if (b = f ? c.mapData : g ? c.mapLayoutData : c.mapScratch, f = f || g || m, !h.color && !h.number) return !1;
                        var x = this.parse(e, f[4]);
                        if (!x || x.mapped) return !1;
                        var w = this.parse(e, f[5]);
                        if (!w || w.mapped) return !1;
                        if (x.value === w.value) return !1;
                        if (h.color) {
                            var E = x.value,
                                _ = w.value,
                                S = !(E[0] !== _[0] || E[1] !== _[1] || E[2] !== _[2] || E[3] !== _[3] && (null != E[3] && 1 !== E[3] || null != _[3] && 1 !== _[3]));
                            if (S) return !1
                        }
                        return {
                            name: e,
                            value: f,
                            strValue: "" + t,
                            mapped: b,
                            field: f[1],
                            fieldMin: parseFloat(f[2]),
                            fieldMax: parseFloat(f[3]),
                            valueMin: x.value,
                            valueMax: w.value,
                            bypass: r
                        }
                    }
                }
                if (h.multiple && "multiple" !== o) {
                    var P;
                    if (P = d ? t.split(/\s+/) : i.array(t) ? t : [t], h.evenMultiple && P.length % 2 !== 0) return null;
                    var T = P.map(function (t) {
                        var n = s.parse(e, t, r, "multiple");
                        return null != n.pfValue ? n.pfValue : n.value
                    });
                    return {
                        name: e,
                        value: T,
                        pfValue: T,
                        strValue: T.join(" "),
                        bypass: r,
                        units: h.number && !h.unitless ? h.implicitUnits || "px" : void 0
                    }
                }
                var k = function () {
                    for (var n = 0; n < h.enums.length; n++) {
                        var i = h.enums[n];
                        if (i === t) return {
                            name: e,
                            value: t,
                            strValue: "" + t,
                            bypass: r
                        }
                    }
                    return null
                };
                if (h.number) {
                    var D, C = "px";
                    if (h.units && (D = h.units), h.implicitUnits && (C = h.implicitUnits), !h.unitless)
                        if (d) {
                            var M = "px|em" + (h.allowPercent ? "|\\%" : "");
                            D && (M = D);
                            var N = t.match("^(" + n.regex.number + ")(" + M + ")?$");
                            N && (t = N[1], D = N[2] || C)
                        } else D && !h.implicitUnits || (D = C);
                    if (t = parseFloat(t), isNaN(t) && void 0 === h.enums) return null;
                    if (isNaN(t) && void 0 !== h.enums) return t = u, k();
                    if (h.integer && !i.integer(t)) return null;
                    if (void 0 !== h.min && t < h.min || void 0 !== h.max && t > h.max) return null;
                    var B = {
                        name: e,
                        value: t,
                        strValue: "" + t + (D ? D : ""),
                        units: D,
                        bypass: r
                    };
                    return h.unitless || "px" !== D && "em" !== D ? B.pfValue = t : B.pfValue = "px" !== D && D ? this.getEmSizeInPixels() * t : t, "ms" !== D && "s" !== D || (B.pfValue = "ms" === D ? t : 1e3 * t), "deg" !== D && "rad" !== D || (B.pfValue = "rad" === D ? t : a.deg2rad(t)), B
                }
                if (h.propList) {
                    var z = [],
                        I = "" + t;
                    if ("none" === I);
                    else {
                        for (var L = I.split(","), O = 0; O < L.length; O++) {
                            var A = L[O].trim();
                            s.properties[A] && z.push(A)
                        }
                        if (0 === z.length) return null
                    }
                    return {
                        name: e,
                        value: z,
                        strValue: 0 === z.length ? "none" : z.join(", "),
                        bypass: r
                    }
                }
                if (h.color) {
                    var R = n.color2tuple(t);
                    return R ? {
                        name: e,
                        value: R,
                        strValue: "" + t,
                        bypass: r,
                        roundValue: !0
                    } : null
                }
                if (h.regex || h.regexes) {
                    if (h.enums) {
                        var q = k();
                        if (q) return q
                    }
                    for (var V = h.regexes ? h.regexes : [h.regex], O = 0; O < V.length; O++) {
                        var F = new RegExp(V[O]),
                            j = F.exec(t);
                        if (j) return {
                            name: e,
                            value: j,
                            strValue: "" + t,
                            bypass: r
                        }
                    }
                    return null
                }
                return h.string ? {
                    name: e,
                    value: "" + t,
                    strValue: "" + t,
                    bypass: r
                } : h.enums ? k() : null
            };
            o.parseImpl = s, t.exports = o
        }, {
            "../is": 83,
            "../math": 85,
            "../util": 100
        }],
        95: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = {};
            ! function () {
                var e = n.regex.number,
                    t = n.regex.rgbaNoBackRefs,
                    r = n.regex.hslaNoBackRefs,
                    a = n.regex.hex3,
                    o = n.regex.hex6,
                    s = function (e) {
                        return "^" + e + "\\s*\\(\\s*([\\w\\.]+)\\s*\\)$"
                    },
                    l = function (n) {
                        var i = e + "|\\w+|" + t + "|" + r + "|" + a + "|" + o;
                        return "^" + n + "\\s*\\(([\\w\\.]+)\\s*\\,\\s*(" + e + ")\\s*\\,\\s*(" + e + ")\\s*,\\s*(" + i + ")\\s*\\,\\s*(" + i + ")\\)$"
                    };
                i.types = {
                    time: {
                        number: !0,
                        min: 0,
                        units: "s|ms",
                        implicitUnits: "ms"
                    },
                    percent: {
                        number: !0,
                        min: 0,
                        max: 100,
                        units: "%",
                        implicitUnits: "%"
                    },
                    zeroOneNumber: {
                        number: !0,
                        min: 0,
                        max: 1,
                        unitless: !0
                    },
                    nOneOneNumber: {
                        number: !0,
                        min: -1,
                        max: 1,
                        unitless: !0
                    },
                    nonNegativeInt: {
                        number: !0,
                        min: 0,
                        integer: !0,
                        unitless: !0
                    },
                    position: {
                        enums: ["parent", "origin"]
                    },
                    nodeSize: {
                        number: !0,
                        min: 0,
                        enums: ["label"]
                    },
                    number: {
                        number: !0,
                        unitless: !0
                    },
                    numbers: {
                        number: !0,
                        unitless: !0,
                        multiple: !0
                    },
                    size: {
                        number: !0,
                        min: 0
                    },
                    bidirectionalSize: {
                        number: !0
                    },
                    bidirectionalSizes: {
                        number: !0,
                        multiple: !0
                    },
                    bgSize: {
                        number: !0,
                        min: 0,
                        allowPercent: !0
                    },
                    bgWH: {
                        number: !0,
                        min: 0,
                        allowPercent: !0,
                        enums: ["auto"]
                    },
                    bgPos: {
                        number: !0,
                        allowPercent: !0
                    },
                    bgRepeat: {
                        enums: ["repeat", "repeat-x", "repeat-y", "no-repeat"]
                    },
                    bgFit: {
                        enums: ["none", "contain", "cover"]
                    },
                    bgCrossOrigin: {
                        enums: ["anonymous", "use-credentials"]
                    },
                    bgClip: {
                        enums: ["none", "node"]
                    },
                    color: {
                        color: !0
                    },
                    bool: {
                        enums: ["yes", "no"]
                    },
                    lineStyle: {
                        enums: ["solid", "dotted", "dashed"]
                    },
                    borderStyle: {
                        enums: ["solid", "dotted", "dashed", "double"]
                    },
                    curveStyle: {
                        enums: ["bezier", "unbundled-bezier", "haystack", "segments"]
                    },
                    fontFamily: {
                        regex: '^([\\w- \\"]+(?:\\s*,\\s*[\\w- \\"]+)*)$'
                    },
                    fontVariant: {
                        enums: ["small-caps", "normal"]
                    },
                    fontStyle: {
                        enums: ["italic", "normal", "oblique"]
                    },
                    fontWeight: {
                        enums: ["normal", "bold", "bolder", "lighter", "100", "200", "300", "400", "500", "600", "800", "900", 100, 200, 300, 400, 500, 600, 700, 800, 900]
                    },
                    textDecoration: {
                        enums: ["none", "underline", "overline", "line-through"]
                    },
                    textTransform: {
                        enums: ["none", "uppercase", "lowercase"]
                    },
                    textWrap: {
                        enums: ["none", "wrap"]
                    },
                    textBackgroundShape: {
                        enums: ["rectangle", "roundrectangle"]
                    },
                    nodeShape: {
                        enums: ["rectangle", "roundrectangle", "ellipse", "triangle", "square", "pentagon", "hexagon", "heptagon", "octagon", "star", "diamond", "vee", "rhomboid", "polygon"]
                    },
                    compoundIncludeLabels: {
                        enums: ["include", "exclude"]
                    },
                    arrowShape: {
                        enums: ["tee", "triangle", "triangle-tee", "triangle-backcurve", "half-triangle-overshot", "vee", "square", "circle", "diamond", "none"]
                    },
                    arrowFill: {
                        enums: ["filled", "hollow"]
                    },
                    display: {
                        enums: ["element", "none"]
                    },
                    visibility: {
                        enums: ["hidden", "visible"]
                    },
                    valign: {
                        enums: ["top", "center", "bottom"]
                    },
                    halign: {
                        enums: ["left", "center", "right"]
                    },
                    text: {
                        string: !0
                    },
                    data: {
                        mapping: !0,
                        regex: s("data")
                    },
                    layoutData: {
                        mapping: !0,
                        regex: s("layoutData")
                    },
                    scratch: {
                        mapping: !0,
                        regex: s("scratch")
                    },
                    mapData: {
                        mapping: !0,
                        regex: l("mapData")
                    },
                    mapLayoutData: {
                        mapping: !0,
                        regex: l("mapLayoutData")
                    },
                    mapScratch: {
                        mapping: !0,
                        regex: l("mapScratch")
                    },
                    fn: {
                        mapping: !0,
                        fn: !0
                    },
                    url: {
                        regex: "url\\s*\\(\\s*['\"]?(.+?)['\"]?\\s*\\)|none|(.+)$"
                    },
                    propList: {
                        propList: !0
                    },
                    angle: {
                        number: !0,
                        units: "deg|rad",
                        implicitUnits: "rad"
                    },
                    textRotation: {
                        number: !0,
                        units: "deg|rad",
                        implicitUnits: "rad",
                        enums: ["none", "autorotate"]
                    },
                    polygonPointList: {
                        number: !0,
                        multiple: !0,
                        evenMultiple: !0,
                        min: -1,
                        max: 1,
                        unitless: !0
                    },
                    edgeDistances: {
                        enums: ["intersection", "node-position"]
                    },
                    easing: {
                        regexes: ["^(spring)\\s*\\(\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*\\)$", "^(cubic-bezier)\\s*\\(\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*,\\s*(" + e + ")\\s*\\)$"],
                        enums: ["linear", "ease", "ease-in", "ease-out", "ease-in-out", "ease-in-sine", "ease-out-sine", "ease-in-out-sine", "ease-in-quad", "ease-out-quad", "ease-in-out-quad", "ease-in-cubic", "ease-out-cubic", "ease-in-out-cubic", "ease-in-quart", "ease-out-quart", "ease-in-out-quart", "ease-in-quint", "ease-out-quint", "ease-in-out-quint", "ease-in-expo", "ease-out-expo", "ease-in-out-expo", "ease-in-circ", "ease-out-circ", "ease-in-out-circ"]
                    }
                };
                var u = i.types,
                    c = i.properties = [{
                        name: "label",
                        type: u.text
                    }, {
                        name: "text-rotation",
                        type: u.textRotation
                    }, {
                        name: "text-margin-x",
                        type: u.bidirectionalSize
                    }, {
                        name: "text-margin-y",
                        type: u.bidirectionalSize
                    }, {
                        name: "source-label",
                        type: u.text
                    }, {
                        name: "source-text-rotation",
                        type: u.textRotation
                    }, {
                        name: "source-text-margin-x",
                        type: u.bidirectionalSize
                    }, {
                        name: "source-text-margin-y",
                        type: u.bidirectionalSize
                    }, {
                        name: "source-text-offset",
                        type: u.size
                    }, {
                        name: "target-label",
                        type: u.text
                    }, {
                        name: "target-text-rotation",
                        type: u.textRotation
                    }, {
                        name: "target-text-margin-x",
                        type: u.bidirectionalSize
                    }, {
                        name: "target-text-margin-y",
                        type: u.bidirectionalSize
                    }, {
                        name: "target-text-offset",
                        type: u.size
                    }, {
                        name: "text-valign",
                        type: u.valign
                    }, {
                        name: "text-halign",
                        type: u.halign
                    }, {
                        name: "color",
                        type: u.color
                    }, {
                        name: "text-outline-color",
                        type: u.color
                    }, {
                        name: "text-outline-width",
                        type: u.size
                    }, {
                        name: "text-outline-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "text-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "text-background-color",
                        type: u.color
                    }, {
                        name: "text-background-margin",
                        type: u.size
                    }, {
                        name: "text-background-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "text-border-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "text-border-color",
                        type: u.color
                    }, {
                        name: "text-border-width",
                        type: u.size
                    }, {
                        name: "text-border-style",
                        type: u.borderStyle
                    }, {
                        name: "text-background-shape",
                        type: u.textBackgroundShape
                    }, {
                        name: "text-transform",
                        type: u.textTransform
                    }, {
                        name: "text-wrap",
                        type: u.textWrap
                    }, {
                        name: "text-max-width",
                        type: u.size
                    }, {
                        name: "text-events",
                        type: u.bool
                    }, {
                        name: "font-family",
                        type: u.fontFamily
                    }, {
                        name: "font-style",
                        type: u.fontStyle
                    }, {
                        name: "font-weight",
                        type: u.fontWeight
                    }, {
                        name: "font-size",
                        type: u.size
                    }, {
                        name: "min-zoomed-font-size",
                        type: u.size
                    }, {
                        name: "events",
                        type: u.bool
                    }, {
                        name: "display",
                        type: u.display
                    }, {
                        name: "visibility",
                        type: u.visibility
                    }, {
                        name: "opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "z-index",
                        type: u.nonNegativeInt
                    }, {
                        name: "overlay-padding",
                        type: u.size
                    }, {
                        name: "overlay-color",
                        type: u.color
                    }, {
                        name: "overlay-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "shadow-blur",
                        type: u.size
                    }, {
                        name: "shadow-color",
                        type: u.color
                    }, {
                        name: "shadow-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "shadow-offset-x",
                        type: u.bidirectionalSize
                    }, {
                        name: "shadow-offset-y",
                        type: u.bidirectionalSize
                    }, {
                        name: "text-shadow-blur",
                        type: u.size
                    }, {
                        name: "text-shadow-color",
                        type: u.color
                    }, {
                        name: "text-shadow-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "text-shadow-offset-x",
                        type: u.bidirectionalSize
                    }, {
                        name: "text-shadow-offset-y",
                        type: u.bidirectionalSize
                    }, {
                        name: "transition-property",
                        type: u.propList
                    }, {
                        name: "transition-duration",
                        type: u.time
                    }, {
                        name: "transition-delay",
                        type: u.time
                    }, {
                        name: "transition-timing-function",
                        type: u.easing
                    }, {
                        name: "height",
                        type: u.nodeSize
                    }, {
                        name: "width",
                        type: u.nodeSize
                    }, {
                        name: "shape",
                        type: u.nodeShape
                    }, {
                        name: "shape-polygon-points",
                        type: u.polygonPointList
                    }, {
                        name: "background-color",
                        type: u.color
                    }, {
                        name: "background-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "background-blacken",
                        type: u.nOneOneNumber
                    }, {
                        name: "padding",
                        type: u.size
                    }, {
                        name: "border-color",
                        type: u.color
                    }, {
                        name: "border-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "border-width",
                        type: u.size
                    }, {
                        name: "border-style",
                        type: u.borderStyle
                    }, {
                        name: "background-image",
                        type: u.url
                    }, {
                        name: "background-image-crossorigin",
                        type: u.bgCrossOrigin
                    }, {
                        name: "background-image-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "background-position-x",
                        type: u.bgPos
                    }, {
                        name: "background-position-y",
                        type: u.bgPos
                    }, {
                        name: "background-repeat",
                        type: u.bgRepeat
                    }, {
                        name: "background-fit",
                        type: u.bgFit
                    }, {
                        name: "background-clip",
                        type: u.bgClip
                    }, {
                        name: "background-width",
                        type: u.bgWH
                    }, {
                        name: "background-height",
                        type: u.bgWH
                    }, {
                        name: "position",
                        type: u.position
                    }, {
                        name: "compound-sizing-wrt-labels",
                        type: u.compoundIncludeLabels
                    }, {
                        name: "line-style",
                        type: u.lineStyle
                    }, {
                        name: "line-color",
                        type: u.color
                    }, {
                        name: "curve-style",
                        type: u.curveStyle
                    }, {
                        name: "haystack-radius",
                        type: u.zeroOneNumber
                    }, {
                        name: "control-point-step-size",
                        type: u.size
                    }, {
                        name: "control-point-distances",
                        type: u.bidirectionalSizes
                    }, {
                        name: "control-point-weights",
                        type: u.numbers
                    }, {
                        name: "segment-distances",
                        type: u.bidirectionalSizes
                    }, {
                        name: "segment-weights",
                        type: u.numbers
                    }, {
                        name: "edge-distances",
                        type: u.edgeDistances
                    }, {
                        name: "selection-box-color",
                        type: u.color
                    }, {
                        name: "selection-box-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "selection-box-border-color",
                        type: u.color
                    }, {
                        name: "selection-box-border-width",
                        type: u.size
                    }, {
                        name: "active-bg-color",
                        type: u.color
                    }, {
                        name: "active-bg-opacity",
                        type: u.zeroOneNumber
                    }, {
                        name: "active-bg-size",
                        type: u.size
                    }, {
                        name: "outside-texture-bg-color",
                        type: u.color
                    }, {
                        name: "outside-texture-bg-opacity",
                        type: u.zeroOneNumber
                    }],
                    d = i.aliases = [{
                        name: "content",
                        pointsTo: "label"
                    }, {
                        name: "control-point-distance",
                        pointsTo: "control-point-distances"
                    }, {
                        name: "control-point-weight",
                        pointsTo: "control-point-weights"
                    }, {
                        name: "edge-text-rotation",
                        pointsTo: "text-rotation"
                    }, {
                        name: "padding-left",
                        pointsTo: "padding"
                    }, {
                        name: "padding-right",
                        pointsTo: "padding"
                    }, {
                        name: "padding-top",
                        pointsTo: "padding"
                    }, {
                        name: "padding-bottom",
                        pointsTo: "padding"
                    }];
                i.pieBackgroundN = 16, c.push({
                    name: "pie-size",
                    type: u.bgSize
                });
                for (var h = 1; h <= i.pieBackgroundN; h++) c.push({
                    name: "pie-" + h + "-background-color",
                    type: u.color
                }), c.push({
                    name: "pie-" + h + "-background-size",
                    type: u.percent
                }), c.push({
                    name: "pie-" + h + "-background-opacity",
                    type: u.zeroOneNumber
                });
                var p = i.arrowPrefixes = ["source", "mid-source", "target", "mid-target"];
                [{
                    name: "arrow-shape",
                    type: u.arrowShape
                }, {
                    name: "arrow-color",
                    type: u.color
                }, {
                    name: "arrow-fill",
                    type: u.arrowFill
                }].forEach(function (e) {
                    p.forEach(function (t) {
                        var r = t + "-" + e.name,
                            n = e.type;
                        c.push({
                            name: r,
                            type: n
                        })
                    })
                }, {}), i.propertyNames = c.map(function (e) {
                    return e.name
                });
                for (var h = 0; h < c.length; h++) {
                    var f = c[h];
                    c[f.name] = f
                }
                for (var h = 0; h < d.length; h++) {
                    var v = d[h],
                        g = c[v.pointsTo],
                        y = {
                            name: v.name,
                            alias: !0,
                            pointsTo: g
                        };
                    c.push(y), c[v.name] = y
                }
            }(), i.getDefaultProperty = function (e) {
                return this.getDefaultProperties()[e]
            }, i.getDefaultProperties = n.memoize(function () {
                for (var e = n.extend({
                    events: "yes",
                    "text-events": "no",
                    "text-valign": "top",
                    "text-halign": "center",
                    color: "#000",
                    "text-outline-color": "#000",
                    "text-outline-width": 0,
                    "text-outline-opacity": 1,
                    "text-opacity": 1,
                    "text-decoration": "none",
                    "text-transform": "none",
                    "text-wrap": "none",
                    "text-max-width": 9999,
                    "text-background-color": "#000",
                    "text-background-opacity": 0,
                    "text-background-margin": 0,
                    "text-border-opacity": 0,
                    "text-border-width": 0,
                    "text-border-style": "solid",
                    "text-border-color": "#000",
                    "text-background-shape": "rectangle",
                    "font-family": "Helvetica Neue, Helvetica, sans-serif",
                    "font-style": "normal",
                    "font-weight": "normal",
                    "font-size": 16,
                    "min-zoomed-font-size": 0,
                    "text-rotation": "none",
                    "source-text-rotation": "none",
                    "target-text-rotation": "none",
                    visibility: "visible",
                    display: "element",
                    opacity: 1,
                    "z-index": 0,
                    label: "",
                    "text-margin-x": 0,
                    "text-margin-y": 0,
                    "source-label": "",
                    "source-text-offset": 0,
                    "source-text-margin-x": 0,
                    "source-text-margin-y": 0,
                    "target-label": "",
                    "target-text-offset": 0,
                    "target-text-margin-x": 0,
                    "target-text-margin-y": 0,
                    "overlay-opacity": 0,
                    "overlay-color": "#000",
                    "overlay-padding": 10,
                    "shadow-opacity": 0,
                    "shadow-color": "#000",
                    "shadow-blur": 10,
                    "shadow-offset-x": 0,
                    "shadow-offset-y": 0,
                    "text-shadow-opacity": 0,
                    "text-shadow-color": "#000",
                    "text-shadow-blur": 5,
                    "text-shadow-offset-x": 0,
                    "text-shadow-offset-y": 0,
                    "transition-property": "none",
                    "transition-duration": 0,
                    "transition-delay": 0,
                    "transition-timing-function": "linear",
                    "background-blacken": 0,
                    "background-color": "#999",
                    "background-opacity": 1,
                    "background-image": "none",
                    "background-image-crossorigin": "anonymous",
                    "background-image-opacity": 1,
                    "background-position-x": "50%",
                    "background-position-y": "50%",
                    "background-repeat": "no-repeat",
                    "background-fit": "none",
                    "background-clip": "node",
                    "background-width": "auto",
                    "background-height": "auto",
                    "border-color": "#000",
                    "border-opacity": 1,
                    "border-width": 0,
                    "border-style": "solid",
                    height: 30,
                    width: 30,
                    shape: "ellipse",
                    "shape-polygon-points": "-1, -1,   1, -1,   1, 1,   -1, 1",
                    padding: 0,
                    position: "origin",
                    "compound-sizing-wrt-labels": "include"
                }, {
                        "pie-size": "100%"
                    }, [{
                        name: "pie-{{i}}-background-color",
                        value: "black"
                    }, {
                        name: "pie-{{i}}-background-size",
                        value: "0%"
                    }, {
                        name: "pie-{{i}}-background-opacity",
                        value: 1
                    }].reduce(function (e, t) {
                        for (var r = 1; r <= i.pieBackgroundN; r++) {
                            var n = t.name.replace("{{i}}", r),
                                a = t.value;
                            e[n] = a
                        }
                        return e
                    }, {}), {
                        "line-style": "solid",
                        "line-color": "#999",
                        "control-point-step-size": 40,
                        "control-point-weights": .5,
                        "segment-weights": .5,
                        "segment-distances": 20,
                        "edge-distances": "intersection",
                        "curve-style": "bezier",
                        "haystack-radius": 0
                    }, [{
                        name: "arrow-shape",
                        value: "none"
                    }, {
                        name: "arrow-color",
                        value: "#999"
                    }, {
                        name: "arrow-fill",
                        value: "filled"
                    }].reduce(function (e, t) {
                        return i.arrowPrefixes.forEach(function (r) {
                            var n = r + "-" + t.name,
                                i = t.value;
                            e[n] = i
                        }), e
                    }, {})), t = {}, r = 0; r < this.properties.length; r++) {
                    var a = this.properties[r];
                    if (!a.pointsTo) {
                        var o = a.name,
                            s = e[o],
                            l = this.parse(o, s);
                        t[o] = l
                    }
                }
                return t
            }), i.addDefaultStylesheet = function () {
                this.selector("$node > node").css({
                    shape: "rectangle",
                    padding: 10,
                    "background-color": "#eee",
                    "border-color": "#ccc",
                    "border-width": 1
                }).selector("edge").css({
                    width: 3,
                    "curve-style": "haystack"
                }).selector(":selected").css({
                    "background-color": "#0169D9",
                    "line-color": "#0169D9",
                    "source-arrow-color": "#0169D9",
                    "target-arrow-color": "#0169D9",
                    "mid-source-arrow-color": "#0169D9",
                    "mid-target-arrow-color": "#0169D9"
                }).selector("node:parent:selected").css({
                    "background-color": "#CCE1F9",
                    "border-color": "#aec8e5"
                }).selector(":active").css({
                    "overlay-color": "black",
                    "overlay-padding": 10,
                    "overlay-opacity": .25
                }).selector("core").css({
                    "selection-box-color": "#ddd",
                    "selection-box-opacity": .65,
                    "selection-box-border-color": "#aaa",
                    "selection-box-border-width": 1,
                    "active-bg-color": "black",
                    "active-bg-opacity": .15,
                    "active-bg-size": 30,
                    "outside-texture-bg-color": "#000",
                    "outside-texture-bg-opacity": .125
                }), this.defaultLength = this.length
            }, t.exports = i
        }, {
            "../util": 100
        }],
        96: [function (e, t, r) {
            "use strict";
            var n = e("../util"),
                i = e("../selector"),
                a = {};
            a.applyFromString = function (e) {
                function t() {
                    c = c.length > a.length ? c.substr(a.length) : ""
                }

                function r() {
                    o = o.length > s.length ? o.substr(s.length) : ""
                }
                var a, o, s, l = this,
                    u = this,
                    c = "" + e;
                for (c = c.replace(/[\/][*](\s|.)+?[*][\/]/g, ""); ;) {
                    var d = c.match(/^\s*$/);
                    if (d) break;
                    var h = c.match(/^\s*((?:.|\s)+?)\s*\{((?:.|\s)+?)\}/);
                    if (!h) {
                        n.error("Halting stylesheet parsing: String stylesheet contains more to parse but no selector and block found in: " + c);
                        break
                    }
                    a = h[0];
                    var p = h[1];
                    if ("core" !== p) {
                        var f = new i(p);
                        if (f._private.invalid) {
                            n.error("Skipping parsing of block: Invalid selector found in string stylesheet: " + p), t();
                            continue
                        }
                    }
                    var v = h[2],
                        g = !1;
                    o = v;
                    for (var y = []; ;) {
                        var d = o.match(/^\s*$/);
                        if (d) break;
                        var m = o.match(/^\s*(.+?)\s*:\s*(.+?)\s*;/);
                        if (!m) {
                            n.error("Skipping parsing of block: Invalid formatting of style property and value definitions found in:" + v), g = !0;
                            break
                        }
                        s = m[0];
                        var b = m[1],
                            x = m[2],
                            w = l.properties[b];
                        if (w) {
                            var E = u.parse(b, x);
                            E ? (y.push({
                                name: b,
                                val: x
                            }), r()) : (n.error("Skipping property: Invalid property definition in: " + s), r())
                        } else n.error("Skipping property: Invalid property name in: " + s), r()
                    }
                    if (g) {
                        t();
                        break
                    }
                    u.selector(p);
                    for (var _ = 0; _ < y.length; _++) {
                        var w = y[_];
                        u.css(w.name, w.val)
                    }
                    t()
                }
                return u
            }, a.fromString = function (e) {
                var t = this;
                return t.resetToDefault(), t.applyFromString(e), t
            }, t.exports = a
        }, {
            "../selector": 87,
            "../util": 100
        }],
        97: [function (e, t, r) {
            "use strict";
            var n = e("./is"),
                i = e("./util"),
                a = e("./style"),
                o = function () {
                    return this instanceof o ? void (this.length = 0) : new o
                },
                s = o.prototype;
            s.instanceString = function () {
                return "stylesheet"
            }, s.selector = function (e) {
                var t = this.length++;
                return this[t] = {
                    selector: e,
                    properties: []
                }, this
            }, s.css = function (e, t) {
                var r = this.length - 1;
                if (n.string(e)) this[r].properties.push({
                    name: e,
                    value: t
                });
                else if (n.plainObject(e))
                    for (var o = e, s = 0; s < a.properties.length; s++) {
                        var l = a.properties[s],
                            u = o[l.name];
                        if (void 0 === u && (u = o[i.dash2camel(l.name)]), void 0 !== u) {
                            var e = l.name,
                                t = u;
                            this[r].properties.push({
                                name: e,
                                value: t
                            })
                        }
                    }
                return this
            }, s.style = s.css, s.generateStyle = function (e) {
                for (var t = new a(e), r = 0; r < this.length; r++) {
                    var n = this[r],
                        i = n.selector,
                        o = n.properties;
                    t.selector(i);
                    for (var s = 0; s < o.length; s++) {
                        var l = o[s];
                        t.css(l.name, l.value)
                    }
                }
                return t
            }, t.exports = o
        }, {
            "./is": 83,
            "./style": 92,
            "./util": 100
        }],
        98: [function (_dereq_, module, exports) { /*! Weaver licensed under MIT (https://tldrlegal.com/license/mit-license), copyright Max Franz */
            "use strict";
            var window = _dereq_("./window"),
                util = _dereq_("./util"),
                Promise = _dereq_("./promise"),
                Event = _dereq_("./event"),
                define = _dereq_("./define"),
                is = _dereq_("./is"),
                Thread = function (e) {
                    if (!(this instanceof Thread)) return new Thread(e);
                    var t = this._private = {
                        requires: [],
                        files: [],
                        queue: null,
                        pass: [],
                        disabled: !1
                    };
                    is.plainObject(e) && null != e.disabled && (t.disabled = !!e.disabled)
                },
                thdfn = Thread.prototype,
                stringifyFieldVal = function (e) {
                    var t = is.fn(e) ? e.toString() : "JSON.parse('" + JSON.stringify(e) + "')";
                    return t
                },
                fnAsRequire = function (e) {
                    var t, r;
                    is.object(e) && e.fn ? (t = fnAs(e.fn, e.name), r = e.name, e = e.fn) : is.fn(e) ? (t = e.toString(), r = e.name) : is.string(e) ? t = e : is.object(e) && (t = e.proto ? "" : e.name + " = {};", r = e.name, e = e.obj), t += "\n";
                    var n = function (e, r) {
                        if (e.prototype) {
                            var n = !1;
                            for (var i in e.prototype) {
                                n = !0;
                                break
                            }
                            n && (t += fnAsRequire({
                                name: r,
                                obj: e,
                                proto: !0
                            }, e))
                        }
                    };
                    if (e.prototype && null != r)
                        for (var i in e.prototype) {
                            var a = "",
                                o = e.prototype[i],
                                s = stringifyFieldVal(o),
                                l = r + ".prototype." + i;
                            a += l + " = " + s + ";\n", a && (t += a), n(o, l)
                        }
                    if (!is.string(e))
                        for (var i in e) {
                            var u = "";
                            if (e.hasOwnProperty(i)) {
                                var o = e[i],
                                    s = stringifyFieldVal(o),
                                    l = r + '["' + i + '"]';
                                u += l + " = " + s + ";\n"
                            }
                            u && (t += u), n(o, l)
                        }
                    return t
                },
                isPathStr = function (e) {
                    return is.string(e) && e.match(/\.js$/)
                };
            util.extend(thdfn, {
                instanceString: function () {
                    return "thread"
                },
                require: function (e, t) {
                    var r = this._private.requires;
                    if (isPathStr(e)) return this._private.files.push(e), this;
                    if (t) e = is.fn(e) ? {
                        name: t,
                        fn: e
                    } : {
                            name: t,
                            obj: e
                        };
                    else if (is.fn(e)) {
                        if (!e.name) throw 'The function name could not be automatically determined.  Use thread.require( someFunction, "someFunction" )';
                        e = {
                            name: e.name,
                            fn: e
                        }
                    }
                    return r.push(e), this
                },
                pass: function (e) {
                    return this._private.pass.push(e), this
                },
                run: function (fn, pass) {
                    var self = this,
                        _p = this._private;
                    if (pass = pass || _p.pass.shift(), _p.stopped) throw "Attempted to run a stopped thread!  Start a new thread or do not stop the existing thread and reuse it.";
                    if (_p.running) return _p.queue = _p.queue.then(function () {
                        return self.run(fn, pass)
                    });
                    var useWW = null != window && !_p.disabled,
                        useNode = !window && "undefined" != typeof module && !_p.disabled;
                    self.trigger("run");
                    var runP = new Promise(function (resolve, reject) {
                        _p.running = !0;
                        var threadTechAlreadyExists = _p.ran,
                            fnImplStr = is.string(fn) ? fn : fn.toString(),
                            fnStr = "\n" + _p.requires.map(function (e) {
                                return fnAsRequire(e)
                            }).concat(_p.files.map(function (e) {
                                if (useWW) {
                                    var t = function (e) {
                                        return e.match(/^\.\//) || e.match(/^\.\./) ? window.location.origin + window.location.pathname + e : e.match(/^\//) ? window.location.origin + "/" + e : e
                                    };
                                    return 'importScripts("' + t(e) + '");'
                                }
                                if (useNode) return 'eval( require("fs").readFileSync("' + e + '", { encoding: "utf8" }) );';
                                throw "External file `" + e + "` can not be required without any threading technology."
                            })).concat(["( function(){", "var ret = (" + fnImplStr + ")(" + JSON.stringify(pass) + ");", "if( ret !== undefined ){ resolve(ret); }", "} )()\n"]).join("\n");
                        if (_p.requires = [], _p.files = [], useWW) {
                            var fnBlob, fnUrl;
                            if (!threadTechAlreadyExists) {
                                var fnPre = fnStr + "";
                                fnStr = ["function _ref_(o){ return eval(o); };", "function broadcast(m){ return message(m); };", "function message(m){ postMessage(m); };", "function listen(fn){", '  self.addEventListener("message", function(m){ ', '    if( typeof m === "object" && (m.data.$$eval || m.data === "$$start") ){', "    } else { ", "      fn( m.data );", "    }", "  });", "};", 'self.addEventListener("message", function(m){  if( m.data.$$eval ){ eval( m.data.$$eval ); }  });', "function resolve(v){ postMessage({ $$resolve: v }); };", "function reject(v){ postMessage({ $$reject: v }); };"].join("\n"), fnStr += fnPre, fnBlob = new Blob([fnStr], {
                                    type: "application/javascript"
                                }), fnUrl = window.URL.createObjectURL(fnBlob)
                            }
                            var ww = _p.webworker = _p.webworker || new Worker(fnUrl);
                            threadTechAlreadyExists && ww.postMessage({
                                $$eval: fnStr
                            });
                            var cb;
                            ww.addEventListener("message", cb = function (e) {
                                var t = is.object(e) && is.object(e.data);
                                t && "$$resolve" in e.data ? (ww.removeEventListener("message", cb), resolve(e.data.$$resolve)) : t && "$$reject" in e.data ? (ww.removeEventListener("message", cb), reject(e.data.$$reject)) : self.trigger(new Event(e, {
                                    type: "message",
                                    message: e.data
                                }))
                            }, !1), threadTechAlreadyExists || ww.postMessage("$$start")
                        } else if (useNode) {
                            _p.child || (_p.child = _dereq_("child_process").fork(_dereq_("path").join(__dirname, "thread-node-fork")));
                            var child = _p.child,
                                cb;
                            child.on("message", cb = function (e) {
                                is.object(e) && "$$resolve" in e ? (child.removeListener("message", cb), resolve(e.$$resolve)) : is.object(e) && "$$reject" in e ? (child.removeListener("message", cb), reject(e.$$reject)) : self.trigger(new Event({}, {
                                    type: "message",
                                    message: e
                                }))
                            }), child.send({
                                $$eval: fnStr
                            })
                        } else {
                            var promiseResolve = resolve,
                                promiseReject = reject,
                                timer = _p.timer = _p.timer || {
                                    listeners: [],
                                    exec: function () {
                                        fnStr = ["function _ref_(o){ return eval(o); };", "function broadcast(m){ return message(m); };", 'function message(m){ self.trigger( new Event({}, { type: "message", message: m }) ); };', "function listen(fn){ timer.listeners.push( fn ); };", "function resolve(v){ promiseResolve(v); };", "function reject(v){ promiseReject(v); };"].join("\n") + fnStr, eval(fnStr)
                                    },
                                    message: function (e) {
                                        for (var t = timer.listeners, r = 0; r < t.length; r++) {
                                            var n = t[r];
                                            n(e)
                                        }
                                    }
                                };
                            timer.exec()
                        }
                    }).then(function (e) {
                        return _p.running = !1, _p.ran = !0, self.trigger("ran"), e
                    });
                    return null == _p.queue && (_p.queue = runP), runP
                },
                message: function (e) {
                    var t = this._private;
                    return t.webworker && t.webworker.postMessage(e), t.child && t.child.send(e), t.timer && t.timer.message(e), this
                },
                stop: function () {
                    var e = this._private;
                    return e.webworker && e.webworker.terminate(), e.child && e.child.kill(), e.timer, e.stopped = !0, this.trigger("stop")
                },
                stopped: function () {
                    return this._private.stopped
                }
            });
            var fnAs = function (e, t) {
                var r = e.toString();
                return r = r.replace(/function\s*?\S*?\s*?\(/, "function " + t + "(")
            },
                defineFnal = function (e) {
                    return e = e || {},
                        function (t, r) {
                            var n = fnAs(t, "_$_$_" + e.name);
                            return this.require(n), this.run(["function( data ){", "  var origResolve = resolve;", "  var res = [];", "  ", "  resolve = function( val ){", "    res.push( val );", "  };", "  ", "  var ret = data." + e.name + "( _$_$_" + e.name + (arguments.length > 1 ? ", " + JSON.stringify(r) : "") + " );", "  ", "  resolve = origResolve;", "  resolve( res.length > 0 ? res : ret );", "}"].join("\n"))
                        }
                };
            util.extend(thdfn, {
                reduce: defineFnal({
                    name: "reduce"
                }),
                reduceRight: defineFnal({
                    name: "reduceRight"
                }),
                map: defineFnal({
                    name: "map"
                })
            });
            var fn = thdfn;
            fn.promise = fn.run, fn.terminate = fn.halt = fn.stop, fn.include = fn.require, util.extend(thdfn, {
                on: define.on(),
                one: define.on({
                    unbindSelfOnTrigger: !0
                }),
                off: define.off(),
                trigger: define.trigger()
            }), define.eventAliasesOn(thdfn), module.exports = Thread
        }, {
            "./define": 44,
            "./event": 45,
            "./is": 83,
            "./promise": 86,
            "./util": 100,
            "./window": 107,
            child_process: void 0,
            path: void 0
        }],
        99: [function (e, t, r) {
            "use strict";
            var n = e("../is");
            t.exports = {
                hex2tuple: function (e) {
                    if ((4 === e.length || 7 === e.length) && "#" === e[0]) {
                        var t, r, n, i = 4 === e.length,
                            a = 16;
                        return i ? (t = parseInt(e[1] + e[1], a), r = parseInt(e[2] + e[2], a), n = parseInt(e[3] + e[3], a)) : (t = parseInt(e[1] + e[2], a), r = parseInt(e[3] + e[4], a), n = parseInt(e[5] + e[6], a)), [t, r, n]
                    }
                },
                hsl2tuple: function (e) {
                    function t(e, t, r) {
                        return 0 > r && (r += 1), r > 1 && (r -= 1), 1 / 6 > r ? e + 6 * (t - e) * r : .5 > r ? t : 2 / 3 > r ? e + (t - e) * (2 / 3 - r) * 6 : e
                    }
                    var r, n, i, a, o, s, l, u, c = new RegExp("^" + this.regex.hsla + "$").exec(e);
                    if (c) {
                        if (n = parseInt(c[1]), 0 > n ? n = (360 - -1 * n % 360) % 360 : n > 360 && (n %= 360), n /= 360, i = parseFloat(c[2]), 0 > i || i > 100) return;
                        if (i /= 100, a = parseFloat(c[3]), 0 > a || a > 100) return;
                        if (a /= 100, o = c[4], void 0 !== o && (o = parseFloat(o), 0 > o || o > 1)) return;
                        if (0 === i) s = l = u = Math.round(255 * a);
                        else {
                            var d = .5 > a ? a * (1 + i) : a + i - a * i,
                                h = 2 * a - d;
                            s = Math.round(255 * t(h, d, n + 1 / 3)), l = Math.round(255 * t(h, d, n)), u = Math.round(255 * t(h, d, n - 1 / 3))
                        }
                        r = [s, l, u, o]
                    }
                    return r
                },
                rgb2tuple: function (e) {
                    var t, r = new RegExp("^" + this.regex.rgba + "$").exec(e);
                    if (r) {
                        t = [];
                        for (var n = [], i = 1; 3 >= i; i++) {
                            var a = r[i];
                            if ("%" === a[a.length - 1] && (n[i] = !0), a = parseFloat(a), n[i] && (a = a / 100 * 255), 0 > a || a > 255) return;
                            t.push(Math.floor(a))
                        }
                        var o = n[1] || n[2] || n[3],
                            s = n[1] && n[2] && n[3];
                        if (o && !s) return;
                        var l = r[4];
                        if (void 0 !== l) {
                            if (l = parseFloat(l), 0 > l || l > 1) return;
                            t.push(l)
                        }
                    }
                    return t
                },
                colorname2tuple: function (e) {
                    return this.colors[e.toLowerCase()]
                },
                color2tuple: function (e) {
                    return (n.array(e) ? e : null) || this.colorname2tuple(e) || this.hex2tuple(e) || this.rgb2tuple(e) || this.hsl2tuple(e)
                },
                colors: {
                    transparent: [0, 0, 0, 0],
                    aliceblue: [240, 248, 255],
                    antiquewhite: [250, 235, 215],
                    aqua: [0, 255, 255],
                    aquamarine: [127, 255, 212],
                    azure: [240, 255, 255],
                    beige: [245, 245, 220],
                    bisque: [255, 228, 196],
                    black: [0, 0, 0],
                    blanchedalmond: [255, 235, 205],
                    blue: [0, 0, 255],
                    blueviolet: [138, 43, 226],
                    brown: [165, 42, 42],
                    burlywood: [222, 184, 135],
                    cadetblue: [95, 158, 160],
                    chartreuse: [127, 255, 0],
                    chocolate: [210, 105, 30],
                    coral: [255, 127, 80],
                    cornflowerblue: [100, 149, 237],
                    cornsilk: [255, 248, 220],
                    crimson: [220, 20, 60],
                    cyan: [0, 255, 255],
                    darkblue: [0, 0, 139],
                    darkcyan: [0, 139, 139],
                    darkgoldenrod: [184, 134, 11],
                    darkgray: [169, 169, 169],
                    darkgreen: [0, 100, 0],
                    darkgrey: [169, 169, 169],
                    darkkhaki: [189, 183, 107],
                    darkmagenta: [139, 0, 139],
                    darkolivegreen: [85, 107, 47],
                    darkorange: [255, 140, 0],
                    darkorchid: [153, 50, 204],
                    darkred: [139, 0, 0],
                    darksalmon: [233, 150, 122],
                    darkseagreen: [143, 188, 143],
                    darkslateblue: [72, 61, 139],
                    darkslategray: [47, 79, 79],
                    darkslategrey: [47, 79, 79],
                    darkturquoise: [0, 206, 209],
                    darkviolet: [148, 0, 211],
                    deeppink: [255, 20, 147],
                    deepskyblue: [0, 191, 255],
                    dimgray: [105, 105, 105],
                    dimgrey: [105, 105, 105],
                    dodgerblue: [30, 144, 255],
                    firebrick: [178, 34, 34],
                    floralwhite: [255, 250, 240],
                    forestgreen: [34, 139, 34],
                    fuchsia: [255, 0, 255],
                    gainsboro: [220, 220, 220],
                    ghostwhite: [248, 248, 255],
                    gold: [255, 215, 0],
                    goldenrod: [218, 165, 32],
                    gray: [128, 128, 128],
                    grey: [128, 128, 128],
                    green: [0, 128, 0],
                    greenyellow: [173, 255, 47],
                    honeydew: [240, 255, 240],
                    hotpink: [255, 105, 180],
                    indianred: [205, 92, 92],
                    indigo: [75, 0, 130],
                    ivory: [255, 255, 240],
                    khaki: [240, 230, 140],
                    lavender: [230, 230, 250],
                    lavenderblush: [255, 240, 245],
                    lawngreen: [124, 252, 0],
                    lemonchiffon: [255, 250, 205],
                    lightblue: [173, 216, 230],
                    lightcoral: [240, 128, 128],
                    lightcyan: [224, 255, 255],
                    lightgoldenrodyellow: [250, 250, 210],
                    lightgray: [211, 211, 211],
                    lightgreen: [144, 238, 144],
                    lightgrey: [211, 211, 211],
                    lightpink: [255, 182, 193],
                    lightsalmon: [255, 160, 122],
                    lightseagreen: [32, 178, 170],
                    lightskyblue: [135, 206, 250],
                    lightslategray: [119, 136, 153],
                    lightslategrey: [119, 136, 153],
                    lightsteelblue: [176, 196, 222],
                    lightyellow: [255, 255, 224],
                    lime: [0, 255, 0],
                    limegreen: [50, 205, 50],
                    linen: [250, 240, 230],
                    magenta: [255, 0, 255],
                    maroon: [128, 0, 0],
                    mediumaquamarine: [102, 205, 170],
                    mediumblue: [0, 0, 205],
                    mediumorchid: [186, 85, 211],
                    mediumpurple: [147, 112, 219],
                    mediumseagreen: [60, 179, 113],
                    mediumslateblue: [123, 104, 238],
                    mediumspringgreen: [0, 250, 154],
                    mediumturquoise: [72, 209, 204],
                    mediumvioletred: [199, 21, 133],
                    midnightblue: [25, 25, 112],
                    mintcream: [245, 255, 250],
                    mistyrose: [255, 228, 225],
                    moccasin: [255, 228, 181],
                    navajowhite: [255, 222, 173],
                    navy: [0, 0, 128],
                    oldlace: [253, 245, 230],
                    olive: [128, 128, 0],
                    olivedrab: [107, 142, 35],
                    orange: [255, 165, 0],
                    orangered: [255, 69, 0],
                    orchid: [218, 112, 214],
                    palegoldenrod: [238, 232, 170],
                    palegreen: [152, 251, 152],
                    paleturquoise: [175, 238, 238],
                    palevioletred: [219, 112, 147],
                    papayawhip: [255, 239, 213],
                    peachpuff: [255, 218, 185],
                    peru: [205, 133, 63],
                    pink: [255, 192, 203],
                    plum: [221, 160, 221],
                    powderblue: [176, 224, 230],
                    purple: [128, 0, 128],
                    red: [255, 0, 0],
                    rosybrown: [188, 143, 143],
                    royalblue: [65, 105, 225],
                    saddlebrown: [139, 69, 19],
                    salmon: [250, 128, 114],
                    sandybrown: [244, 164, 96],
                    seagreen: [46, 139, 87],
                    seashell: [255, 245, 238],
                    sienna: [160, 82, 45],
                    silver: [192, 192, 192],
                    skyblue: [135, 206, 235],
                    slateblue: [106, 90, 205],
                    slategray: [112, 128, 144],
                    slategrey: [112, 128, 144],
                    snow: [255, 250, 250],
                    springgreen: [0, 255, 127],
                    steelblue: [70, 130, 180],
                    tan: [210, 180, 140],
                    teal: [0, 128, 128],
                    thistle: [216, 191, 216],
                    tomato: [255, 99, 71],
                    turquoise: [64, 224, 208],
                    violet: [238, 130, 238],
                    wheat: [245, 222, 179],
                    white: [255, 255, 255],
                    whitesmoke: [245, 245, 245],
                    yellow: [255, 255, 0],
                    yellowgreen: [154, 205, 50]
                }
            }
        }, {
            "../is": 83
        }],
        100: [function (e, t, r) {
            "use strict";
            var n = e("../is"),
                i = e("../math"),
                a = {
                    trueify: function () {
                        return !0
                    },
                    falsify: function () {
                        return !1
                    },
                    zeroify: function () {
                        return 0
                    },
                    noop: function () { },
                    error: function (e) {
                        console.error ? (console.error.apply(console, arguments), console.trace && console.trace()) : (console.log.apply(console, arguments), console.trace && console.trace())
                    },
                    clone: function (e) {
                        return this.extend({}, e)
                    },
                    copy: function (e) {
                        return null == e ? e : n.array(e) ? e.slice() : n.plainObject(e) ? this.clone(e) : e
                    },
                    uuid: function (e, t) {
                        for (t = e = ""; e++ < 36; t += 51 * e & 52 ? (15 ^ e ? 8 ^ Math.random() * (20 ^ e ? 16 : 4) : 4).toString(16) : "-");
                        return t
                    }
                };
            a.makeBoundingBox = i.makeBoundingBox.bind(i), a._staticEmptyObject = {}, a.staticEmptyObject = function () {
                return a._staticEmptyObject
            }, a.extend = null != Object.assign ? Object.assign : function (e) {
                for (var t = arguments, r = 1; r < t.length; r++) {
                    var n = t[r];
                    if (n)
                        for (var i = Object.keys(n), a = 0; a < i.length; a++) {
                            var o = i[a];
                            e[o] = n[o]
                        }
                }
                return e
            }, a["default"] = function (e, t) {
                return void 0 === e ? t : e
            }, a.removeFromArray = function (e, t, r) {
                for (var n = e.length; n >= 0 && (e[n] !== t || (e.splice(n, 1), r)); n--);
            }, a.clearArray = function (e) {
                e.splice(0, e.length)
            }, a.getPrefixedProperty = function (e, t, r) {
                return r && (t = this.prependCamel(r, t)), e[t]
            }, a.setPrefixedProperty = function (e, t, r, n) {
                r && (t = this.prependCamel(r, t)), e[t] = n
            }, [e("./colors"), e("./maps"), {
                memoize: e("./memoize")
            }, e("./regex"), e("./strings"), e("./timing")].forEach(function (e) {
                a.extend(a, e)
            }), t.exports = a
        }, {
            "../is": 83,
            "../math": 85,
            "./colors": 99,
            "./maps": 101,
            "./memoize": 102,
            "./regex": 103,
            "./strings": 104,
            "./timing": 105
        }],
        101: [function (e, t, r) {
            "use strict";
            var n = e("../is");
            t.exports = {
                mapEmpty: function (e) {
                    var t = !0;
                    return null != e ? 0 === Object.keys(e).length : t
                },
                pushMap: function (e) {
                    var t = this.getMap(e);
                    null == t ? this.setMap(this.extend({}, e, {
                        value: [e.value]
                    })) : t.push(e.value)
                },
                setMap: function (e) {
                    for (var t, r = e.map, i = e.keys, a = i.length, o = 0; a > o; o++) {
                        var t = i[o];
                        n.plainObject(t) && this.error("Tried to set map with object key"), o < i.length - 1 ? (null == r[t] && (r[t] = {}), r = r[t]) : r[t] = e.value
                    }
                },
                getMap: function (e) {
                    for (var t = e.map, r = e.keys, i = r.length, a = 0; i > a; a++) {
                        var o = r[a];
                        if (n.plainObject(o) && this.error("Tried to get map with object key"), t = t[o], null == t) return t
                    }
                    return t
                },
                deleteMap: function (e) {
                    for (var t = e.map, r = e.keys, i = r.length, a = e.keepChildren, o = 0; i > o; o++) {
                        var s = r[o];
                        n.plainObject(s) && this.error("Tried to delete map with object key");
                        var l = o === e.keys.length - 1;
                        if (l)
                            if (a)
                                for (var u = Object.keys(t), c = 0; c < u.length; c++) {
                                    var d = u[c];
                                    a[d] || (t[d] = void 0)
                                } else t[s] = void 0;
                        else t = t[s]
                    }
                }
            }
        }, {
            "../is": 83
        }],
        102: [function (e, t, r) {
            "use strict";
            t.exports = function (e, t) {
                t || (t = function () {
                    if (1 === arguments.length) return arguments[0];
                    if (0 === arguments.length) return "undefined";
                    for (var e = [], t = 0; t < arguments.length; t++) e.push(arguments[t]);
                    return e.join("$")
                });
                var r = function () {
                    var n, i = this,
                        a = arguments,
                        o = t.apply(i, a),
                        s = r.cache;
                    return (n = s[o]) || (n = s[o] = e.apply(i, a)), n
                };
                return r.cache = {}, r
            }
        }, {}],
        103: [function (e, t, r) {
            "use strict";
            var n = "(?:[-+]?(?:(?:\\d+|\\d*\\.\\d+)(?:[Ee][+-]?\\d+)?))",
                i = "rgb[a]?\\((" + n + "[%]?)\\s*,\\s*(" + n + "[%]?)\\s*,\\s*(" + n + "[%]?)(?:\\s*,\\s*(" + n + "))?\\)",
                a = "rgb[a]?\\((?:" + n + "[%]?)\\s*,\\s*(?:" + n + "[%]?)\\s*,\\s*(?:" + n + "[%]?)(?:\\s*,\\s*(?:" + n + "))?\\)",
                o = "hsl[a]?\\((" + n + ")\\s*,\\s*(" + n + "[%])\\s*,\\s*(" + n + "[%])(?:\\s*,\\s*(" + n + "))?\\)",
                s = "hsl[a]?\\((?:" + n + ")\\s*,\\s*(?:" + n + "[%])\\s*,\\s*(?:" + n + "[%])(?:\\s*,\\s*(?:" + n + "))?\\)",
                l = "\\#[0-9a-fA-F]{3}",
                u = "\\#[0-9a-fA-F]{6}";
            t.exports = {
                regex: {
                    number: n,
                    rgba: i,
                    rgbaNoBackRefs: a,
                    hsla: o,
                    hslaNoBackRefs: s,
                    hex3: l,
                    hex6: u
                }
            }
        }, {}],
        104: [function (e, t, r) {
            "use strict";
            var n = e("./memoize"),
                i = e("../is");
            t.exports = {
                camel2dash: n(function (e) {
                    return e.replace(/([A-Z])/g, function (e) {
                        return "-" + e.toLowerCase()
                    })
                }),
                dash2camel: n(function (e) {
                    return e.replace(/(-\w)/g, function (e) {
                        return e[1].toUpperCase()
                    })
                }),
                prependCamel: n(function (e, t) {
                    return e + t[0].toUpperCase() + t.substring(1)
                }, function (e, t) {
                    return e + "$" + t
                }),
                capitalize: function (e) {
                    return i.emptyString(e) ? e : e.charAt(0).toUpperCase() + e.substring(1)
                }
            }
        }, {
            "../is": 83,
            "./memoize": 102
        }],
        105: [function (e, t, r) {
            "use strict";
            var n = e("../window"),
                i = e("../is"),
                a = n ? n.performance : null,
                o = {},
                s = n ? function () {
                    return n.requestAnimationFrame ? function (e) {
                        n.requestAnimationFrame(e)
                    } : n.mozRequestAnimationFrame ? function (e) {
                        n.mozRequestAnimationFrame(e)
                    } : n.webkitRequestAnimationFrame ? function (e) {
                        n.webkitRequestAnimationFrame(e)
                    } : n.msRequestAnimationFrame ? function (e) {
                        n.msRequestAnimationFrame(e)
                    } : void 0
                }() : function (e) {
                    e && setTimeout(function () {
                        e(l())
                    }, 1e3 / 60)
                };
            o.requestAnimationFrame = function (e) {
                s(e)
            };
            var l = a && a.now ? function () {
                return a.now()
            } : function () {
                return Date.now()
            };
            o.performanceNow = l, o.throttle = function (e, t, r) {
                var n = !0,
                    a = !0;
                return r === !1 ? n = !1 : i.plainObject(r) && (n = "leading" in r ? r.leading : n, a = "trailing" in r ? r.trailing : a), r = r || {}, r.leading = n, r.maxWait = t, r.trailing = a, o.debounce(e, t, r)
            }, o.now = function () {
                return Date.now()
            }, o.debounce = function (e, t, r) {
                var n, a, o, s, l, u, c, d = this,
                    h = 0,
                    p = !1,
                    f = !0;
                if (i.fn(e)) {
                    if (t = Math.max(0, t) || 0, r === !0) {
                        var v = !0;
                        f = !1
                    } else i.plainObject(r) && (v = r.leading, p = "maxWait" in r && (Math.max(t, r.maxWait) || 0), f = "trailing" in r ? r.trailing : f);
                    var g = function () {
                        var r = t - (d.now() - s);
                        if (0 >= r) {
                            a && clearTimeout(a);
                            var i = c;
                            a = u = c = void 0, i && (h = d.now(), o = e.apply(l, n), u || a || (n = l = null))
                        } else u = setTimeout(g, r)
                    },
                        y = function () {
                            u && clearTimeout(u), a = u = c = void 0, (f || p !== t) && (h = d.now(), o = e.apply(l, n), u || a || (n = l = null))
                        };
                    return function () {
                        if (n = arguments, s = d.now(), l = this, c = f && (u || !v), p === !1) var r = v && !u;
                        else {
                            a || v || (h = s);
                            var i = p - (s - h),
                                m = 0 >= i;
                            m ? (a && (a = clearTimeout(a)), h = s, o = e.apply(l, n)) : a || (a = setTimeout(y, i))
                        }
                        return m && u ? u = clearTimeout(u) : u || t === p || (u = setTimeout(g, t)), r && (m = !0, o = e.apply(l, n)), !m || u || a || (n = l = null), o
                    }
                }
            }, t.exports = o
        }, {
            "../is": 83,
            "../window": 107
        }],
        106: [function (e, t, r) {
            t.exports = "2.7.16"
        }, {}],
        107: [function (e, t, r) {
            t.exports = "undefined" == typeof window ? null : window
        }, {}]
    }, {}, [82])(82)
});