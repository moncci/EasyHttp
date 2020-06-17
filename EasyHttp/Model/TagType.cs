using System;
using System.Collections.Generic;
using System.Text;

namespace EasyHttp.Model
{
    /// <summary>
    /// 标签类型
    /// </summary>
    public enum TagType
    {
        html = 0,
        head = 1,
        body = 2,
        a = 3,
        b = 4,
        br = 5,
        button = 6,
        canvas = 7,
        div = 8,
        font = 9,
        footer = 10,
        form = 11,
        frame = 12,
        h1 = 13,
        h2 = 14,
        h3 = 15,
        h4 = 16,
        h5 = 17,
        h6 = 18,
        header = 19,
        hr = 20,
        iframe = 21,
        img = 22,
        input = 23,
        label = 24,
        li = 25,
        link = 26,
        nav = 27,
        p = 28,
        script = 29,
        select = 30,
        span = 31,
        style = 32,
        table = 33,
        tbody = 34,
        td = 35,
        textarea = 36,
        tfoot = 37,
        th = 38,
        thead = 39,
        title = 40,
        tr = 41,
        ul = 42,

        /// <summary>
        /// 没有明确指定标签
        /// </summary>
        none = 99999
    }
}
