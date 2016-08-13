using System;

/// <summary>
/// Debugクラスに関する汎用関数
/// </summary>
public static class DebugCommon
{
    /// <summary>
    /// 条件をチェックし、その条件が false の場合は例外を投げます
    /// </summary>
    /// <param name="condition">評価する条件式</param>
    public static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// 条件をチェックし、その条件が false の場合は例外を投げます
    /// </summary>
    /// <param name="condition">評価する条件式</param>
    /// <param name="message">送信するメッセージ</param>
    public static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception(message);
        }
    }

    /// <summary>
    /// 条件をチェックし、その条件が false の場合は例外を投げます
    /// </summary>
    /// <param name="condition">評価する条件式</param>
    /// <param name="getMessage">送信するメッセージを返す関数</param>
    public static void Assert(bool condition, Func<string> getMessage)
    {
        if (!condition)
        {
            throw new Exception(getMessage());
        }
    }
}
