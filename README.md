# DesignCrowd.TechnicalChallenge

This project is a .NET 8 class library that provides functionality to calculate business days and weekdays between two dates, taking into account public holidays.

## Projects

- **Class Library:** `DesignCrowd.TechnicalChallenge`
- **Unit Test Project:** `DesignCrowd.TechnicalChallenge.Tests`

## Overview

The class library includes the following key components:

### BusinessDayCounter Class

The `BusinessDayCounter` class contains methods for calculating weekdays and business days between two dates.

#### Methods

1. **WeekdaysBetweenTwoDates(DateTime startDate, DateTime endDate)**: 
   - **Returns:** `int` - The number of weekdays between the two given dates (exclusive).
  
2. **BusinessDaysBetweenTwoDates(DateTime startDate, DateTime endDate, IList<DateTime> publicHolidays)**:
   - **Returns:** `int` - The number of business days between the two given dates (exclusive), excluding public holidays from the specified list.
  
3. **BusinessDaysBetweenTwoDates(DateTime startDate, DateTime endDate, IEnumerable<PublicHoliday> publicHolidays)**:
   - **Returns:** `int` - The number of business days between the two given dates (exclusive), considering different types of public holidays specified by the `PublicHoliday` classes.

### PublicHoliday Class

The `PublicHoliday` class is an abstract class that provides the following methods:

1. **GetDate(int year)**: 
   - **Returns:** `DateTime` - The date of the public holiday for the given calendar year.
  
2. **GetAdditionalDate(int year, IEnumerable<PublicHoliday> publicHolidays)**: 
   - **Returns:** `DateTime?` - The additional date of the public holiday if it falls on a weekend and is applicable for adjustment to the next non-holiday weekday. Returns `NULL` if not applicable.

### Inherited Classes

The `PublicHoliday` class has the following subclasses:

1. **DayOfWeekPublicHoliday**:
   - Represents a public holiday on a certain occurrence of a certain day in a month (e.g., Queen's Birthday on the second Monday in June every year).

2. **EasterSunday**:
   - Represents the holiday of Easter Sunday.

3. **FixedDatePublicHoliday**:
   - Represents a public holiday on a fixed date (e.g., Anzac Day on April 25th every year). 
   - **Behavior:** If the holiday falls on a weekend or another public holiday, and `_substituted` is `true`, it will move to the next non-holiday weekday; otherwise, it remains as-is.

4. **RelativeDayOfWeekPublicHoliday**:
   - Represents a public holiday on a relative day to another public holiday (e.g., Good Friday on the most recent Friday before Easter Sunday).

## Installation

To use the `DesignCrowd.TechnicalChallenge` library, you can clone this repository and build it in your .NET 8 environment.

## Run the project

Choose to add an existing project, and choose `DesignCrowd.TechnicalChallenge` library, use the `BusinessDayCounter` claas as mentioned above.

Alternatively open the project as-is and from `Test Explorer` run all tests or specific test.