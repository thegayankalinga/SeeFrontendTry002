# Classical Machine Learning Approach for Software Effort Estimation

## Introduction

Classical machine learning models remain powerful tools for software effort estimation, offering interpretability and efficiency. Our research implements and evaluates traditional ML algorithms to predict effort in person-days across four key categories: delivery, engineering, DevOps, and QA in the FinTech domain.

## Problem Background

Traditional software effort estimation methods like COCOMO and Function Point Analysis often struggle with the complexity of modern software projects, particularly in specialized domains like FinTech. The dynamic nature of software development, combined with domain-specific requirements, creates a need for more adaptive and data-driven estimation approaches.

## Our Machine Learning Implementation

### Data Preprocessing

Effective data preprocessing was crucial for our model performance:

1. **Missing Value Treatment**: Applied median imputation for numerical features and mode imputation for categorical features
2. **Normalization**: Scaled numerical variables using StandardScaler
3. **Encoding**: Transformed categorical variables using LabelEncoder
4. **Feature Selection**: Identified and utilized the most relevant features for prediction
5. **Data Splitting**: Divided the dataset into training (70%), validation (15%), and test (15%) sets

### Random Forest Implementation

Random Forest was selected for its ability to handle complex, non-linear relationships without overfitting:

```python
def create_rf_model():
    # Create baseline Random Forest model
    baseline_rf = RandomForestRegressor(
        n_estimators=100,
        random_state=42
    )
    
    return baseline_rf

# Train on multi-target data
baseline_rf_model = create_rf_model()
baseline_rf_model.fit(X_train, y_train)

# Make predictions
baseline_rf_preds = baseline_rf_model.predict(X_test)
```

After establishing the baseline, we performed hyperparameter tuning to optimize performance:

```python
# Define hyperparameter grid
param_grid = {
    'n_estimators': [100, 200, 300],
    'max_depth': [None, 10, 20, 30],
    'min_samples_split': [2, 5, 10],
    'min_samples_leaf': [1, 2, 4]
}

# Random search for hyperparameter tuning
random_search = RandomizedSearchCV(
    RandomForestRegressor(random_state=42),
    param_distributions=param_grid,
    n_iter=20,
    cv=3,
    scoring='neg_mean_squared_error',
    random_state=42
)

random_search.fit(X_train, y_train)
best_rf_model = random_search.best_estimator_
```

### XGBoost Implementation

XGBoost was implemented for its gradient boosting capabilities and efficiency:

```python
def create_xgboost_model():
    # Create baseline XGBoost model
    baseline_xgb = XGBRegressor(
        n_estimators=100,
        learning_rate=0.1,
        random_state=42
    )
    
    return baseline_xgb

# Train on multi-target data
baseline_xgb_model = MultiOutputRegressor(create_xgboost_model())
baseline_xgb_model.fit(X_train, y_train)

# Make predictions
baseline_xgb_preds = baseline_xgb_model.predict(X_test)
```

Similar to Random Forest, we performed hyperparameter tuning for XGBoost:

```python
# Define hyperparameter grid for XGBoost
xgb_param_grid = {
    'estimator__n_estimators': [100, 200, 300],
    'estimator__learning_rate': [0.01, 0.05, 0.1],
    'estimator__max_depth': [3, 5, 7],
    'estimator__subsample': [0.8, 0.9, 1.0],
    'estimator__colsample_bytree': [0.8, 0.9, 1.0]
}

# Random search for XGBoost
xgb_random_search = RandomizedSearchCV(
    MultiOutputRegressor(XGBRegressor(random_state=42)),
    param_distributions=xgb_param_grid,
    n_iter=20,
    cv=3,
    scoring='neg_mean_squared_error',
    random_state=42
)

xgb_random_search.fit(X_train, y_train)
best_xgb_model = xgb_random_search.best_estimator_
```

## Evaluation Metrics

We evaluated our models using comprehensive metrics:

1. **Mean Squared Error (MSE)**: Measures the average squared difference between estimated and actual values
2. **Root Mean Squared Error (RMSE)**: The square root of MSE, providing a measure in the same unit as the target variable
3. **R-Squared (R²)**: Indicates the proportion of variance in the dependent variable explained by the model
4. **Mean Absolute Percentage Error (MAPE)**: Measures accuracy as a percentage of error
5. **Mean Magnitude of Relative Error (MMRE)**: Similar to MAPE but focuses on relative error magnitude

```python
def evaluate_model(model, X, y_true, model_name):
    y_pred = model.predict(X)
    
    # Compute metrics for each target
    metrics = {
        'MSE': mean_squared_error(y_true, y_pred),
        'RMSE': np.sqrt(mean_squared_error(y_true, y_pred)),
        'R-Squared': r2_score(y_true, y_pred),
        'MAPE': mean_absolute_percentage_error(y_true, y_pred) * 100,
        'MMRE': np.mean(np.abs((y_true - y_pred) / y_true)) * 100
    }
    
    print(f"\nMetrics for {model_name}:")
    for metric, value in metrics.items():
        print(f"{metric}: {value:.10f}")
    
    return metrics, y_pred
```

## Results and Analysis

Our classical ML models demonstrated impressive performance, with XGBoost showing particularly strong results:

| Model | MSE | RMSE | R-Squared | MAPE (%) | MMRE |
|-------|-----|------|-----------|----------|------|
| Baseline Random Forest | 0.1376138371 | 0.3709633905 | 0.8632087239 | 1026.431126 | 10.26428374 |
| Baseline XGBoost | 0.01785090566 | 0.1336072815 | 0.9822349548 | 453.0071594 | 4.530059321 |
| Optimized Random Forest | 0.1360702093 | 0.3688769569 | 0.8647401857 | 992.5272571 | 9.925246057 |
| Optimized XGBoost | 0.02599972859 | 0.1612443134 | 0.9741230607 | 471.8588285 | 4.718575271 |

The baseline XGBoost model exhibited the best overall performance with the lowest MSE (0.01785) and RMSE (0.1336), along with the highest R-squared value (0.9822). While hyperparameter tuning improved the Random Forest model slightly, it surprisingly didn't enhance the XGBoost model's performance, which suggests the baseline configuration was already well-suited for our dataset.

## Model Deployment

Our classical ML models were saved for production deployment:

```python
# Save the models for deployment
def save_models(models, model_names):
    for model, name in zip(models, model_names):
        filename = f"{results_path}/{name}.pkl"
        joblib.dump(model, filename)
        print(f"Model saved: {filename}")

# Save the best models
save_models(
    [baseline_rf_model, baseline_xgb_model, best_rf_model, best_xgb_model],
    ["baseline_rf", "baseline_xgb", "optimized_rf", "optimized_xgb"]
)
```

For integration with the backend API, we implemented a prediction service:

```python
def load_model(model_path):
    """Load a saved model from disk"""
    return joblib.load(model_path)

def preprocess_input(input_data):
    """Preprocess input data for prediction"""
    # Apply the same preprocessing steps used during training
    # (encoding, scaling, etc.)
    return processed_data

@app.post("/predict/classical", response_model=PredictionResponse)
async def predict_classical(request: PredictionRequest):
    try:
        # Convert input data to DataFrame
        input_data = pd.DataFrame([request.dict()])
        
        # Preprocess the input data
        processed_data = preprocess_input(input_data)
        
        # Make prediction using the best model (XGBoost)
        prediction = xgb_model.predict(processed_data)
        
        # Format the response
        response = {
            "delivery": float(prediction[0][0]),
            "engineering": float(prediction[0][1]),
            "devops": float(prediction[0][2]),
            "qa": float(prediction[0][3]),
            "total": float(np.sum(prediction[0]))
        }
        
        return response
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
```

## Feature Importance

One significant advantage of classical ML models is their interpretability. We analyzed feature importance to understand what factors most influence effort estimation:

```python
def plot_feature_importance(model, feature_names, model_name):
    if hasattr(model, 'feature_importances_'):
        importances = model.feature_importances_
    else:
        # For MultiOutputRegressor, access the base estimator
        importances = model.estimators_[0].feature_importances_
    
    indices = np.argsort(importances)[::-1]
    
    plt.figure(figsize=(10, 6))
    plt.title(f'Feature Importance - {model_name}')
    plt.bar(range(len(indices)), importances[indices], align='center')
    plt.xticks(range(len(indices)), [feature_names[i] for i in indices], rotation=90)
    plt.tight_layout()
    plt.savefig(f"{results_path}/feature_importance_{model_name}.png")
    plt.close()
```

## Challenges and Lessons Learned

1. **Support Vector Regression**: Initially attempted SVR but had to drop it due to excessive training time (2+ hours)
2. **Hyperparameter Tuning**: Grid search proved too computationally intensive, leading us to adopt Random Search with reduced grid and cross-validation size
3. **Model Selection**: XGBoost demonstrated surprising effectiveness even without extensive tuning, suggesting it's particularly well-suited for our domain

## Conclusion

Classical machine learning approaches, particularly XGBoost, demonstrate strong performance for software effort estimation in the FinTech domain. While deep learning methods may offer marginal improvements in accuracy, classical ML models provide the advantages of faster training, easier deployment, and greater interpretability.

For future work, we aim to:
1. Further explore feature engineering to improve model performance
2. Implement model stacking and blending techniques to combine the strengths of different models
3. Conduct more extensive hyperparameter optimization with increased computational resources