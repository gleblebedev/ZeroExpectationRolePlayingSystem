# Zero Expectation Role Playing System
Role playing system based on normal (Gaussian) distribution with zero mean (zero expectation) and unit sigma.

# What is Normal Distribution?

**TL;DR**: Imagine you’re rolling a regular six-sided die. Each number (1 to 6) has an equal chance of showing up. The total you get from rolling two dice (like 2, 3, 4, ..., up to 12) follows a pattern similar to the bell curve. Common totals (like 7) are more probable, while extreme outcomes (like 2 or 12) are rare. In real-world scenarios, data often follows a bell curve. Most values cluster around the **mean**, and extreme values are less common. The **standard deviation** controls how spread out the data is.

The **normal distribution**, also known as the **Gaussian distribution**, is a fundamental concept in probability theory and statistics. Let’s delve into its properties, uses, and advantages.

## Definition and Properties:

The **normal distribution** is symmetric about the **mean**, which means that data near the mean occur more frequently than data far from it.

Graphically, it appears as a bell curve.

### Key features:
- **Mean**, **median**, and **mode** are equal and represent the peak of the distribution.
- The distribution falls symmetrically around the mean.
- Width is defined by the **standard deviation**.
- All normal distributions can be described by just two parameters: the mean and the standard deviation.

### The Empirical Rule:
- Approximately 68.2% of observations fall within one standard deviation of the mean.
- About 95.4% fall within two standard deviations.
- Nearly 99.7% fall within three standard deviations.

![Standard deviation by Ainali, under CC-BY-SA 3.0](https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/Standard_deviation_diagram.svg/400px-Standard_deviation_diagram.svg.png)

This rule helps understand where most data in a normal distribution lies.

## Benefits for a Role Playing System:

- **Symmetry and Bell Shape**: The normal distribution’s symmetric and bell-shaped nature makes it useful for describing various populations, from classroom grades to heights and weights.
- **Predictive Power**: Many naturally occurring phenomena approximate the normal distribution, making it a powerful tool for modeling real-world data.
- **Light Tails**: The normal distribution assumes light tails (low probability of extreme values), which may not hold for certain data sets.

(see more at https://en.wikipedia.org/wiki/Normal_distribution )

## Math behind normal distribution

The formula for the normal probability distribution is as follows:

![normal_distribution](docs/images/normal_distribution.png)

Where:

- **x** represents the normal random variable.
- **μ** (mu) is the mean of the distribution.
- **σ** (sigma) is the standard deviation of the distribution.

Let’s break it down:

The term (1 / sqrt(2 * π * σ^2)) ensures that the area under the curve sums up to 1 (since it’s a probability distribution).

The exponential term (e^((x - μ)^2) / (2 * σ^2)) describes how the data is distributed around the mean. It controls the shape of the bell curve.

The standard normal distribution is a special case where (μ = 0) and (σ = 1). In this case, the formula simplifies to:

![normal_distribution](docs/images/normal_distribution_0_1.png)

# Dice rolls and Skill checks

TBC