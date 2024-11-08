using System;
using System.Linq;
using Google.Protobuf.WellKnownTypes;

namespace Discount.Grpc.Extensions;

public class ProtoConversionExtension
{
    /// <summary>
    /// This method convert DateTime to TimeStamp and reverse way.
    /// Default Mapster is applied transform for this 2 type. This is alternative method.
    /// </summary>
    /// <param name="source">Source Object</param>
    /// <typeparam name="TSource">Source Type</typeparam>
    /// <typeparam name="TTarget">Destination Type</typeparam>
    /// <returns>Destination Object</returns>
    public static TTarget ConvertTo<TSource, TTarget>(TSource source)
        where TTarget : new()
    {
        var target           = new TTarget();
        var sourceProperties = typeof(TSource).GetProperties();
        var targetProperties = typeof(TTarget).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var targetProperty = targetProperties.FirstOrDefault(p =>
                p.Name.Equals(sourceProperty.Name, StringComparison.OrdinalIgnoreCase));

            if (targetProperty == null || !targetProperty.CanWrite)
                continue;

            var value = sourceProperty.GetValue(source);
            if (value == null)
                continue;

            // Handle DateTime to Timestamp conversion
            if (sourceProperty.PropertyType == typeof(DateTime) &&
                targetProperty.PropertyType == typeof(Timestamp))
            {
                var dateTime = (DateTime)value;
                var timestamp = Timestamp.FromDateTime(
                    DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
                targetProperty.SetValue(target, timestamp);
            }
            // Handle Timestamp to DateTime conversion
            else if (sourceProperty.PropertyType == typeof(Timestamp) &&
                     targetProperty.PropertyType == typeof(DateTime))
            {
                var timestamp = (Timestamp)value;
                targetProperty.SetValue(target, timestamp.ToDateTime());
            }
            // Handle other property types that match
            else if (targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
            {
                targetProperty.SetValue(target, value);
            }
        }

        return target;
    }
}
