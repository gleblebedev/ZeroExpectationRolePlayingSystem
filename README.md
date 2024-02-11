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

Simplified normal distribution is used for skill checks and the full formula is used to calculate outcome, like amount of damage.

# Dice rolls and Skill checks

Let’s break down how the RPG skill check works step by step:

### Random Value Generation:

- A new random value is generated. This value represents the inherent randomness and unpredictability of the situation.
- Let’s denote this random value as (**R**).

### Skill Level:

- Each character or player has a skill level associated with the specific skill being tested (e.g., lockpicking, swordsmanship, magic, etc.).
- This skill level reflects their proficiency or expertise in that area.
Let’s denote the skill level as (**S**).

### Difficulty of the Task:

- The difficulty of the task or challenge is also considered.
- This difficulty factor represents how hard it is to accomplish the task.
- Let’s denote the difficulty as (**D**).

### Calculating the Resulting Value:

We calculate the resulting value by subtracting the skill level from the random value and then adding the difficulty: ```Check = R - S + D```

### Success or Failure:

If the resulting value is less than or equal to zero ((R - S + D \leq 0)), the skill check is a success. The character successfully accomplishes the task.

Otherwise, if the resulting value is greater than zero ((R - S + D > 0)), the skill check fails. The character does not succeed in the task.

In summary, the skill check combines randomness, the character’s skill level, and the difficulty of the task to determine whether the character succeeds or fails. It adds an element of chance and strategy to the game, making it more engaging for players.

### Critical success or failure:

In a threshold-based system, we define specific thresholds for success and failure.
For example:

If the resulting value ((R - S + D)) is below a certain threshold (e.g., 0), it’s a regular success.

If the resulting value is above another threshold (e.g., -3.0), it’s a critical success. Remember, negative values are treated as success. The threshold -3.0 is equivalent to 0.135% success rate or once in 741 dice rolls.

Conversely, if the resulting value is less than a failure threshold (e.g., 3.0), it’s a critical failure.

Critical successes and failures occur when the result significantly deviates from the norm. These thresholds can be adjusted based on game balance and desired gameplay.

### Sample dice rolls.

Let's look at a roleplaying system with 3.0 as a threshold value for a critical values.

| S-D | Cirtical Success | 🎲 | Success | 🎲 | Critical Failure  | 🎲 |
|--|--|--|--|--|--|--|
| -2 | 0.000% | 1 of 3488555 | 2.275% | 1 of 43  | 15.866% | 1 of 6 |
| -1.5 | 0.000% | 1 of 294319 | 6.681% | 1 of 14  | 6.681% | 1 of 14 |
| -1 | 0.003% | 1 of 31574 | 15.866% | 1 of 6  | 2.275% | 1 of 43 |
| -0.5 | 0.023% | 1 of 4298 | 30.854% | 1 of 3  | 0.621% | 1 of 161 |
| 0 | 0.135% | 1 of 740 | 50.000% | 1 of 2  | 0.135% | 1 of 740 |
| 0.5 | 0.621% | 1 of 161 | 69.146% | 10 of 14  | 0.023% | 1 of 4298 |
| 1 | 2.275% | 1 of 43 | 84.134% | 10 of 11  | 0.003% | 1 of 31574 |
| 1.5 | 6.681% | 1 of 14 | 93.319% | 100 of 107  | 0.000% | 1 of 294319 |
| 2 | 15.866% | 1 of 6 | 97.725% | 100 of 102  | 0.000% | 1 of 3488555 |

Let’s analyze the skill check for the amateur lock picker picking a hard lock.

#### Skill Level (**S**):

- The amateur lock picker has a skill level of (S = 0.45).
- Skill level of 0.45 is equivalent of 67.3% chance of success on a lock with difficulty D = 0.0, or roughly 10 of 14 successful rolls.

#### Difficulty of the Lock (**D**):
- The lock’s difficulty is (D = 0.7).
- A higher difficulty value indicates a tougher lock to pick. Difficulty equal to 0.7 gives a player with skill level equal to 0.0 a chance to open the lock 24.2% success rate, or roughly 1 of 4 attempts.

#### Dice roll and outcome.

A new random value is generated. Let’s assume it’s **R** = 0.5.

We calculate the resulting value: [ Check = R - S + D = 0.5 - 0.45 + 0.7 = 0.75 ]

The resulting value of 0.75 is greater than zero. In this case, the skill check fails because the lock picker couldn’t successfully open the lock.

The amateur’s lack of skill combined with the moderate difficulty of the lock resulted in failure.

For [ **S-D** = 0.45 - 0.7 = -0.25 ] success rates are

| S-D | Cirtical Success | 🎲 | Success | 🎲 | Critical Failure  | 🎲 |
|--|--|--|--|--|--|--|
| -0.25 | 0.058% | 1 of 1733 | 40.129% | 1 of 2  | 0.298% | 1 of 335 |