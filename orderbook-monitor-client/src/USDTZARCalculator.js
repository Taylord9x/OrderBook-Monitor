import React, { useState, useEffect } from 'react';

const USDTZARCalculator = () => {
  const [usdtAmount, setUsdtAmount] = useState('');
  const [zarPrice, setZarPrice] = useState(null);

  useEffect(() => {
    const fetchPrice = async () => {
      if (usdtAmount && !isNaN(usdtAmount)) {
        try {
          const response = await fetch(`http://localhost:5032/api/price/get-price?quantity=${usdtAmount}`);
          if (response.ok) {
            const data = await response.json();
            setZarPrice(data.price);
          } else {
            console.error('Error fetching price');
            setZarPrice(null);
          }
        } catch (error) {
          console.error('Error:', error);
          setZarPrice(null);
        }
      } else {
        setZarPrice(null);
      }
    };
    fetchPrice();
  }, [usdtAmount]);

  const handleInputChange = (e) => {
    setUsdtAmount(e.target.value);
  };

  return (
    <div className="w-full max-w-md mx-auto mt-10">
      <h2 className="text-2xl font-bold text-center">USDT/ZAR Price Calculator</h2>
      <div className="space-y-4">
        <div>
          <label htmlFor="usdtAmount" className="block text-sm font-medium text-gray-700">
            USDT Amount
          </label>
          <input
            id="usdtAmount"
            type="number"
            value={usdtAmount}
            onChange={handleInputChange}
            placeholder="Enter USDT amount"
            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-300 focus:ring focus:ring-indigo-200 focus:ring-opacity-50"
          />
        </div>
        <div>
          <p className="text-sm font-medium text-gray-700">Estimated ZAR Price:</p>
          <p className="text-2xl font-bold mt-1">
            {zarPrice !== null ? `R ${zarPrice.toFixed(2)}` : '-'}
          </p>
        </div>
      </div>
    </div>
  );
};

export default USDTZARCalculator;