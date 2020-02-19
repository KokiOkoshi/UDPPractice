using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UDPPractice.Converters
{
    /// <summary>
    /// <see cref="IValueConverter"/>の実装ヘルパークラス
    /// </summary>
    /// <typeparam name="TInput">変換元の値の型</typeparam>
    /// <typeparam name="TOutput">変換先の値の型</typeparam>
    abstract class ConverterBase<TInput, TOutput> : IValueConverter
    {
        readonly Type _inputType = typeof(TInput);
        readonly Type _outputType = typeof(TOutput);

        /// <summary>
        /// <see cref="TInput"/>型の値を有効な<see cref="TOutput"/>型の値に変換が可能な場合にのみ変換します
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="result">変換後の値</param>
        /// <param name="parameter">変化に使用する任意のオブジェクト</param>
        /// <param name="culture">変化に使用するカルチャ</param>
        /// <returns>変換に成功したか</returns>
        protected abstract bool TryConvert(TInput value, out TOutput result, object parameter, CultureInfo culture);

        /// <summary>
        /// <see cref="TOutput"/>型の値を有効な<see cref="TInput"/>型の値に変換が可能な場合にのみ変換します
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="result">変換後の値</param>
        /// <param name="parameter">変化に使用する任意のオブジェクト</param>
        /// <param name="culture">変化に使用するカルチャ</param>
        /// <returns>変換に成功したか</returns>
        protected abstract bool TryConvertBack(TOutput value, out TInput result, object parameter, CultureInfo culture);

        /// <summary>
        /// 値を変換します
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="targetType">変換先の型</param>
        /// <param name="parameter">変化に使用する任意のオブジェクト</param>
        /// <param name="culture">変化に使用するカルチャ</param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsInstanceOfType(value)
                || targetType != _inputType
                || !TryConvert((TInput)value, out var result, parameter, culture))
            {
                return DependencyProperty.UnsetValue;
            }

            return result;
        }

        /// <summary>
        /// 値を変換します
        /// </summary>
        /// <param name="value">変換元の値</param>
        /// <param name="targetType">変換先の型</param>
        /// <param name="parameter">変化に使用する任意のオブジェクト</param>
        /// <param name="culture">変化に使用するカルチャ</param>
        /// <returns></returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsInstanceOfType(value)
                || targetType != _outputType
                || !TryConvertBack((TOutput)value, out var result, parameter, culture))
            {
                return DependencyProperty.UnsetValue;
            }

            return result;
        }
    }
}
